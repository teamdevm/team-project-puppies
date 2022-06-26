using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DogsCompanion.Data;
using DogsCompanion.Data.Entities;

namespace DogsCompanion.App.Controllers.Content
{
    [Route("api/content/salons")]
    [ApiController]
    public class GroomerSalonsController : ControllerBase
    {
        private readonly DogsCompanionContext _context;

        public GroomerSalonsController(DogsCompanionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение справочной информации о салонах для собак
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GroomerSalon>>> GetGroomerSalons()
        {
            var salons = await _context.GroomerSalons.OrderBy(g => g.Id).ToListAsync();
            return Ok(salons);
        }

        /// <summary>
        /// Получение справочной информации о конкретном салоне для собак
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroomerSalon>> GetGroomerSalon(int id)
        {
            var groomerSalon = await _context.GroomerSalons.FindAsync(id);

            if (groomerSalon == null)
            {
                return NotFound();
            }

            return Ok(groomerSalon);
        }
    }
}
