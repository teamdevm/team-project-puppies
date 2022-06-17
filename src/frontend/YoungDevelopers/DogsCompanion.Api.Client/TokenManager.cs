using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DogsCompanion.Api.Client
{
    internal static class TokenManager
    {
        private static JwtSecurityTokenHandler _jwtTokenHandler = new JwtSecurityTokenHandler();

        public static JwtSecurityToken GetParsedJwtToken(string accessToken)
        {
            JwtSecurityToken parsedToken = _jwtTokenHandler.ReadJwtToken(accessToken);
            return parsedToken;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dateTime;
        }
    }
}
