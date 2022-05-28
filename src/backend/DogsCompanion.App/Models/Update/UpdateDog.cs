using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Models.Update
{
    public class UpdateDog
    {
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Breed { get; set; }
        public int? Weight { get; set; }
    }
}
