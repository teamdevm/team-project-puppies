using DogsCompanion.App.Models;
using DogsCompanion.App.Models.Account;
using DogsCompanion.App.Settings;
using DogsCompanion.Data;
using DogsCompanion.Data.Entities;
using DogsCompanion.Data.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DogsCompanion.App.Authentication
{
    public class JwtManager
    {
        private readonly SecuritySettings _settings;
        private JwtSecurityTokenHandler _jwtTokenHandler;
        public JwtManager(SecuritySettings settings)
        {
            _settings = settings;
            _jwtTokenHandler = new JwtSecurityTokenHandler();
        }

        public Tokens GenerateTokens(string userId)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(_settings.JwtSecret);

            string jti = Guid.NewGuid().ToString();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(SecurityConstants.ClaimNames.UserId, userId),
                    new Claim(JwtRegisteredClaimNames.Jti, jti),
                    new Claim(JwtRegisteredClaimNames.Aud, _settings.JwtAudience),
                    new Claim(JwtRegisteredClaimNames.Iss, _settings.JwtIssuer)
                }),
                Expires = DateTime.UtcNow.AddDays(_settings.JwtExpireDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);
            string accessToken = jwtTokenHandler.WriteToken(token);
            string refreshToken = GenerateRefreshToken();
            return new Tokens
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Jti = jti,
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public JwtSecurityToken GetParsedJwtToken(string accessToken)
        {
            JwtSecurityToken parsedToken = _jwtTokenHandler.ReadJwtToken(accessToken);
            return parsedToken;
        }

        public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dateTime;
        }
    }
}
