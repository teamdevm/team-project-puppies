using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Settings
{
    public class SecuritySettings
    {
        public string JwtSecret { get; set; } = string.Empty;
        public string JwtIssuer { get; set; } = string.Empty;
        public string JwtAudience { get; set; } = string.Empty;
        public int JwtExpireDays { get; set; }
        public int RefreshTokenExpireDays { get; set; }
    }
}
