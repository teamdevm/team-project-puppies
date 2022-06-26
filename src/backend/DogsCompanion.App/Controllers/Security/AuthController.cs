using DogsCompanion.App.Authentication;
using DogsCompanion.App.Models;
using DogsCompanion.App.Models.Account;
using DogsCompanion.Data;
using DogsCompanion.Data.Entities;
using DogsCompanion.Data.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Controllers.Security
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DogsCompanionContext _context;
        private readonly JwtManager _jwtManager;

        public AuthController(DogsCompanionContext context, JwtManager JWTManager)
        {
            _context = context;
            _jwtManager = JWTManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthInfo authInfo)
        {
            authInfo.Email = authInfo.Email.ToLower();

            var user = await _context.Users.Where(u => u.Email == authInfo.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                return Unauthorized("Invalid Login or email");
            }

            if (!BCrypt.Net.BCrypt.Verify(authInfo.Password, user.Password))
            {
                return Unauthorized("Invalid Login or email");
            }

            Tokens tokens;
            try
            {
                tokens = _jwtManager.GenerateTokens(user.Id.ToString());
                await AddRefreshTokenToDbAsync(tokens.RefreshToken, tokens.Jti, user.Id);
            }
            catch (DbUpdateException updateExc)
            {
                // TODO log: DbUpdateException
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }
            catch (Exception exc)
            {
                //TODO log: Token generation error
                return Unauthorized("Invalid Attempt!");
            }

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                BirthDate = user.BirthDate,
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
            };

            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RefreshTokenRequest>> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var parsedToken = _jwtManager.GetParsedJwtToken(refreshTokenRequest.AccessToken);
            if (parsedToken == null)
            {
                return BadRequest("Cannot parse token");
            }

            var utcExpiryDate = long.Parse(parsedToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expDate = _jwtManager.UnixTimeStampToDateTime(utcExpiryDate);
            if (expDate > DateTime.UtcNow)
            {
                return BadRequest("Cannot refresh access token since it has not expired");
            }

            var storedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshTokenRequest.RefreshToken);
            if (storedRefreshToken == null)
            {
                return BadRequest("Refresh token doesnt exist");
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return BadRequest("Token has expired, user needs to relogin");
            }

            if (storedRefreshToken.IsUsed)
            {
                return BadRequest("Token has been used");
            }

            if (storedRefreshToken.IsRevoked)
            {
                return BadRequest("Token has been revoked");
            }

            var jti = parsedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti);
            if (jti == null || storedRefreshToken.JwtId != jti.Value)
            {
                return BadRequest("No JTI claim");
            }

            if (!int.TryParse(parsedToken.Claims.FirstOrDefault(x => x.Type == SecurityConstants.ClaimNames.UserId).Value, out int userId))
            {
                return BadRequest("No JTI claim");
            }

            var tokens = _jwtManager.GenerateTokens(userId.ToString());
            try
            {
                storedRefreshToken.IsUsed = true;
                _context.RefreshTokens.Update(storedRefreshToken);
                await AddRefreshTokenToDbAsync(tokens.RefreshToken, tokens.Jti, userId);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException updateExc)
            {
                // TODO log: DbUpdateException
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }

            return Ok(new RefreshTokenRequest
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            });
        }

        private async Task<RefreshToken> AddRefreshTokenToDbAsync(string refreshToken, string jti, int userId)
        {
            var dbRefreshToken = new RefreshToken()
            {
                JwtId = jti,
                IsUsed = false,
                UserId = userId,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(30),
                IsRevoked = false,
                Token = refreshToken
            };

            await _context.RefreshTokens.AddAsync(dbRefreshToken);
            await _context.SaveChangesAsync();

            return dbRefreshToken;
        }
    }
}
