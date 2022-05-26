using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsCompanion.Data.Entities
{
    public class Dog
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Breed { get; set; }
        public int? Weight { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
