using DogsCompanion.App.Authentication;
using DogsCompanion.App.Models.Personal;
using DogsCompanion.App.Models.Update;
using DogsCompanion.Data;
using DogsCompanion.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Controllers.Personal
{
    [Authorize]
    [ApiController]
    [Route("api/account/dog")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class DogsController : ControllerBase
    {
        private readonly DogsCompanionContext _context;
        private readonly int? _userId;

        public DogsController(DogsCompanionContext context, ClaimsValidationService claimsValidationService)
        {
            _context = context;
            _userId = claimsValidationService.GetUserId();
        }

        /// <summary>
        /// Получение информации об одной собаке
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReadDog>> GetDogs()
        {
            if (_userId == null)
            {
                return Unauthorized("No JTI claim");
            }
            int userId = _userId!.Value;

            var dog = await _context.Dogs.Where(d => d.UserId == userId).OrderBy(d => d.Id).FirstOrDefaultAsync();
            if (dog == null)
            {
                return NotFound("Dog not found");
            }

            var readDog = new ReadDog
            {
                Id = dog.Id,
                Name = dog.Name,
                BirthDate = dog.BirthDate,
                Breed = dog.Breed,
                Weight = dog.Weight,
                UserId = dog.UserId,
            };
            return Ok(readDog);
        }


        /// <summary>
        /// Обновление данных о собаке
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutDog(UpdateDog updateDog)
        {
            if (_userId == null)
            {
                return Unauthorized("No JTI claim");
            }
            int userId = _userId!.Value;

            var dog = await _context.Dogs.Where(d => d.UserId == userId).OrderBy(d => d.Id).FirstOrDefaultAsync();
            if (dog == null)
            {
                return NotFound("Dog not found");
            }

            dog.Name = updateDog.Name;
            dog.BirthDate = updateDog.BirthDate;
            dog.Breed = updateDog.Breed;
            dog.Weight = updateDog.Weight;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private bool DogExists(int id)
        {
            return _context.Dogs.Any(e => e.Id == id);
        }
    }
}
