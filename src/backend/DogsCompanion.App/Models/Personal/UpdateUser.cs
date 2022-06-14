using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Models.Personal
{
    public class UpdateUser
    {
        [StringLength(30)]
        public string? FirstName { get; set; }

        [StringLength(30)]
        public string? LastName { get; set; }

        [StringLength(30)]
        public string? MiddleName { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
