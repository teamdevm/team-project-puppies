using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace DogsCompanion.Api.Client
{
    public partial class DogsCompanionClient
    {
        public delegate void UpdatedTokenEventHandler(string refreshToken, string accessToken);
        public event UpdatedTokenEventHandler UpdatedToken;

        public delegate void FailedUpdateTokenEventHandler();
        public event FailedUpdateTokenEventHandler FailedUpdateToken;

        public delegate void FailedServerUpdateTokenEventHandler();
        public event FailedServerUpdateTokenEventHandler FailedServerUpdateToken;

        public IToken TokenController { get; set; }

        public DogsCompanionClient(string baseUrl, 
            HttpClient httpClient,
            IToken tokenController) : this(baseUrl, httpClient)
        {
            TokenController = tokenController;
        }

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            string jwt;
            try
            {
                jwt = client.DefaultRequestHeaders.Authorization.Parameter;
            }
            catch (Exception)
            {
                return;
            }

            JwtSecurityToken parsedToken = TokenManager.GetParsedJwtToken(jwt);

            var utcExpiryDate = long.Parse(parsedToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expDate = TokenManager.UnixTimeStampToDateTime(utcExpiryDate);

            if (DateTime.UtcNow < expDate)
            {
                return;
            }

            RefreshToken(jwt);
        }

        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                string jwt;
                try
                {
                    jwt = client.DefaultRequestHeaders.Authorization.Parameter;
                }
                catch (Exception)
                {
                    return;
                }
                JwtSecurityToken parsedToken = TokenManager.GetParsedJwtToken(jwt);

                var utcExpiryDate = long.Parse(parsedToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expDate = TokenManager.UnixTimeStampToDateTime(utcExpiryDate);

                if (DateTime.UtcNow < expDate)
                {
                    return;
                }

                RefreshToken(jwt);
            }
        }

        private void RefreshToken(string jwt)
        {
            using (var httpClient = new HttpClient())
            {
                using (var httpRequest = new HttpRequestMessage())
                {
                    var urlBuilder = new System.Text.StringBuilder();
                    urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/auth/refresh-token");

                    string refreshToken = TokenController.GetRefreshTokenAsync().Result;
                    if (refreshToken == null)
                    {
                        throw new Exception("Cannot get refresh token");
                    }

                    var body = new RefreshTokenRequest
                    {
                        AccessToken = jwt,
                        RefreshToken = refreshToken,
                    };

                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body, _settings.Value));
                    content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    httpRequest.Content = content;
                    httpRequest.Method = new HttpMethod("POST");
                    httpRequest.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                    var url = urlBuilder.ToString();
                    httpRequest.RequestUri = new Uri(url, System.UriKind.RelativeOrAbsolute);

                    var httpResponse = httpClient.SendAsync(httpRequest, System.Net.Http.HttpCompletionOption.ResponseHeadersRead).Result;

                    if (httpResponse.StatusCode == HttpStatusCode.Unauthorized || httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Event log out app
                        FailedUpdateToken?.Invoke();
                        return;
                    }
                    else if ((int)httpResponse.StatusCode >= 500 && (int)httpResponse.StatusCode <= 599)
                    {
                        FailedServerUpdateToken?.Invoke();
                        return;
                    }

                    // Парсинг ответа
                    RefreshTokenRequest tokens = null;
                    var contentStream = httpResponse.Content.ReadAsStreamAsync().Result;
                    using (var streamReader = new StreamReader(contentStream))
                    {
                        using (var jsonReader = new JsonTextReader(streamReader))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            tokens = serializer.Deserialize<RefreshTokenRequest>(jsonReader);
                        }
                    }

                    // Токен удачно обновлен, обновляем по событию в хранилище
                    UpdatedToken?.Invoke(tokens.RefreshToken, tokens.AccessToken);
                }
            }

        }
    }
}
