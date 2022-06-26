using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsCompanion.Data.Entities
{
    public class GroomerSalon
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Link { get; set; }
        public double Rating { get; set; } = 0.0;

        [NotMapped]
        public List<OpeningHours> OpeningHours { get; set; } = new List<OpeningHours>();
    }
}
