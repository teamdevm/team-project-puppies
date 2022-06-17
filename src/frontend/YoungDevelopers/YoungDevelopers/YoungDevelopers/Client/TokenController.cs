using DogsCompanion.Api.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace YoungDevelopers.Client
{
    public class TokenController : IToken
    {
        public async Task<string> GetAccessTokenAsync()
        {
            return await SecureStorage.GetAsync("access_token");
        }

        public async Task SetAccessTokenAsync(string accessToken)
        {
            await SecureStorage.SetAsync("access_token", accessToken);
        }

        public async Task<string> GetRefreshTokenAsync()
        {
            return await SecureStorage.GetAsync("refresh_token");
        }

        public async Task SetRefreshTokenAsync(string refreshToken)
        {
            await SecureStorage.SetAsync("refresh_token", refreshToken);
        }
    }
}
