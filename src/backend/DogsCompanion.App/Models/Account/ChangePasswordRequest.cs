using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DogsCompanion.App.Utils;

namespace DogsCompanion.App.Models.Account
{
    public class ChangePasswordRequest
    {
        [Required]
        [RegularExpression(RegexConstants.PasswordPattern, ErrorMessage = "Password must meet requirements")]
        public string NewPassword { get; set; } = null!;

        [Required]
        public string CurrentPassword { get; set; } = string.Empty;
    }
}
