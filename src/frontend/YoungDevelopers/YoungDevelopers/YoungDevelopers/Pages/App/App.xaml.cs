using DogsCompanion.Api.Client;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using YoungDevelopers.Client;
using YoungDevelopers.Services;

namespace YoungDevelopers
{
    public partial class App : Application
    {

        public App()
        {
            //CreateClient();
            //DataControl.LoadData();
            InitializeComponent();
            //DependencyService.Register<MockDataStore>();
            //MainPage = new NavigationPage(new LoginPage());
        }

        private void CreateClient()
        {
            var httpClient = new HttpClient();
            App.Current.Properties["httpClient"] = httpClient;

            IToken tokenController = new TokenController();
            App.Current.Properties["tokenController"] = new TokenController();

            string baseUrl = "https://exp-webapi.herokuapp.com/";
            var dogsCompanionClient = new DogsCompanionClient(baseUrl, httpClient, tokenController);
            dogsCompanionClient.UpdatedToken += UpdateTokens; // Событие по получению новых токенов
            dogsCompanionClient.FailedServerUpdateToken += FailedServerUpdateToken;

            App.Current.Properties["dogsCompanionClient"] = dogsCompanionClient;
        }

        private async void UpdateTokens(string refreshToken, string accessToken)
        {
            var tokenController = (TokenController)App.Current.Properties["tokenController"];

            await tokenController.SetRefreshTokenAsync(refreshToken);
            await tokenController.SetAccessTokenAsync(accessToken);
        }

        private async void FailedServerUpdateToken()
        {
            var tokenController = (TokenController)App.Current.Properties["tokenController"];
            var refreshTokenRequest = new RefreshTokenRequest()
            {
                RefreshToken = await tokenController.GetRefreshTokenAsync(),
                AccessToken = await tokenController.GetAccessTokenAsync(),
            };

        }

        protected override async void OnStart()
        {
            var httpClient = new HttpClient();
            App.Current.Properties["httpClient"] = httpClient;

            IToken tokenController = new TokenController();
            App.Current.Properties["tokenController"] = new TokenController();

            string baseUrl = "https://exp-webapi.herokuapp.com/";
            var dogsCompanionClient = new DogsCompanionClient(baseUrl, httpClient, tokenController);
            dogsCompanionClient.UpdatedToken += UpdateTokens; // Событие по получению новых токенов
            dogsCompanionClient.FailedServerUpdateToken += FailedServerUpdateToken;

            App.Current.Properties["dogsCompanionClient"] = dogsCompanionClient;

            DataControl.LoadData();

            string accessToken = null;
            try
            {
                accessToken = await tokenController.GetAccessTokenAsync();
            }
            catch (System.Exception) { }

            // Если есть токен, то делаем действия аналогично авторизации
            if (!string.IsNullOrEmpty(accessToken))
            {
                // Установление ключа в httpclient
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                try
                {
                    var userInfo = await dogsCompanionClient.GetUserInfoAsync();
                    var dogInfo = await dogsCompanionClient.GetDogsAsync();

                    var authResponse = new AuthResponse()
                    {
                        Id = userInfo.Id,
                        Email = userInfo.Email,
                        PhoneNumber = userInfo.PhoneNumber,
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        MiddleName = userInfo.MiddleName,
                        BirthDate = userInfo.BirthDate,
                    };
                    DataControl.SetAuthData(authResponse, dogInfo);
                    DataControl.SetUserInfoItem(userInfo);


                    MainPage = new NavigationPage(new LoginPage());
                    App.Current.MainPage = new MainPage();
                }
                catch (System.Exception)
                {
                    
                    App.Current.MainPage = new Page();
                    await Application.Current.MainPage.DisplayAlert("", "Сервис не доступен", "OK");
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnSleep()
        {
            DataControl.SerializeToFile();
        }

        protected override void OnResume()
        {
        }
    }
}
