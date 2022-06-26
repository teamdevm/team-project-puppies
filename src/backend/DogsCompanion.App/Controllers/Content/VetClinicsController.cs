using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DogsCompanion.Data;
using DogsCompanion.Data.Entities;
using DogsCompanion.App.Models.Read;

namespace DogsCompanion.App.Controllers.Content
{
    [Route("api/content/clinics")]
    [ApiController]
    public class VetClinicsController : ControllerBase
    {
        private readonly DogsCompanionContext _context;

        public VetClinicsController(DogsCompanionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка ветклиник
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<VetClinic>>> GetVetClinics()
        {
            var clinics = await _context.VetClinincs.Select(c => new ReadVetClinic
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber,
                Link = c.Link,
                Rating = c.Rating,
                IsAllDay = c.IsAllDay,
                OpeningHours = c.OpeningHours
            }).OrderBy(c => c.Id).ToListAsync();

            return Ok(clinics);
        }

        /// <summary>
        /// Получение информации о ветклинике
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VetClinic>> GetVetClinic(int id)
        {
            var vetClinic = await _context.VetClinincs.FindAsync(id);

            if (vetClinic == null)
            {
                return NotFound();
            }

            return Ok(vetClinic);
        }
    }
}
