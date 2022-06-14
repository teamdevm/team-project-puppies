using DogsCompanion.App.Models.Update;
using DogsCompanion.App.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Models.Account
{
    public class RegisterInfo
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(RegexConstants.PasswordPattern, ErrorMessage = "Password must meet requirements")]
        public string Password { get; set; } = null!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(30)]
        public string? FirstName { get; set; }

        [StringLength(30)]
        public string? LastName { get; set; }

        [StringLength(30)]
        public string? MiddleName { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        public UpdateDog Dog { get; set; } = null!;
    }
}
