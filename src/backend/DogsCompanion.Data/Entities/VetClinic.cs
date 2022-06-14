using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsCompanion.Data.Entities
{
    public class VetClinic
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Link { get; set; }
        public double Rating { get; set; } = 0.0;
        public bool IsAllDay { get; set; } = false;

        [NotMapped]
        public List<OpeningHours> OpeningHours { get; set; } = new List<OpeningHours>();
    }
}
