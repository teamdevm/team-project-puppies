using DogsCompanion.App.Settings;
using DogsCompanion.Data.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;

namespace DogsCompanion.App.Authentication
{
    public class ClaimsValidationService
    {
        private readonly SecuritySettings _settings;
        private readonly HttpContext _httpContext;

        public ClaimsValidationService(IHttpContextAccessor httpContextAccessor, IOptions<SecuritySettings> settings)
        {
            _settings = settings.Value;
            _httpContext = httpContextAccessor.HttpContext
                ?? throw new InvalidOperationException("HttpContext not initialized");
        }

        public int? GetUserId()
        {
            var claim = _httpContext.User.FindFirst(x => x.Type == SecurityConstants.ClaimNames.UserId);
            if (claim == null)
                return null;

            if (int.TryParse(claim.Value, out int userId))
            {
                return userId;
            }
            else
            {
                // TODO log: Failed to parse JTI value
                return null;
            }
        }
    }
}
