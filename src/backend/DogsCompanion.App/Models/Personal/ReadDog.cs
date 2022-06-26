using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Models.Personal
{
    public class ReadDog
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Breed { get; set; }
        public int? Weight { get; set; }
        public int UserId { get; set; }
    }
}
