using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Models.Account.Update
{
    public class UpdateEmail
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
