using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DogsCompanion.App.Utils;

namespace DogsCompanion.App.Models.Account.Update
{
    public class UpdatePassword
    {
        [Required]
        [RegularExpression(RegexConstants.PasswordPattern, ErrorMessage = "Password must meet requirements")]
        public string Password { get; set; } = null!;
    }
}
