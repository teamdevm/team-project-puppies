using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Models.Update
{
    public class UpdateDog
    {
        [StringLength(30)]
        public string? Name { get; set; }
        
        public DateTime? BirthDate { get; set; }

        [StringLength(30)]
        public string? Breed { get; set; }

        [Range(1, 200)]
        public int? Weight { get; set; }
    }
}
