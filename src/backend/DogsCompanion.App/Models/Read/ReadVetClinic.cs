using DogsCompanion.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Models.Read
{
    public class ReadVetClinic
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Link { get; set; }
        public double Rating { get; set; }
        public bool IsAllDay { get; set; } = false;
        public List<OpeningHours> OpeningHours { get; set; } = null!;
    }
}
