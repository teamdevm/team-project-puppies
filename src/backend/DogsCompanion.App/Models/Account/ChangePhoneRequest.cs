using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Models.Account
{
    public class ChangePhoneRequest
    {
        [Required]
        [Phone]
        public string NewPhoneNumber { get; set; } = null!;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
