using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DogsCompanion.Api.Client
{
    public interface IToken
    {
        Task<string> GetAccessTokenAsync();
        Task SetAccessTokenAsync(string accessToken);

        Task<string> GetRefreshTokenAsync();
        Task SetRefreshTokenAsync(string refreshToken);
    }
}
