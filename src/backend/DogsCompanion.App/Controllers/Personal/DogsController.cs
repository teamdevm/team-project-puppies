using DogsCompanion.App.Models.Read;
using DogsCompanion.App.Models.Update;
using DogsCompanion.Data;
using DogsCompanion.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Controllers.Personal
{
    [Route("api/users/{userId}/dogs")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DogsCompanionContext _context;

        public DogsController(DogsCompanionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка собак пользователя
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ReadDog>>> GetDogs(int userId)
        {
            if (!UserExists(userId))
            {
                return NotFound("User not found");
            }

            var dogs = await _context.Dogs.Where(u => u.UserId == userId).Select(d => new ReadDog
            {
                Id = d.Id,
                Name = d.Name,
                BirthDate = d.BirthDate,
                Breed = d.Breed,
                Weight = d.Weight,
                UserId = d.UserId,
            }).ToListAsync();

            return Ok(dogs);
        }

        /// <summary>
        /// Получение информации об одной собаке
        /// </summary>
        [HttpGet("{dogId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReadDog>> GetDogs(int userId, int dogId)
        {
            if (!UserExists(userId))
            {
                return NotFound("User not found");
            }

            var dog = await _context.Dogs.FindAsync(dogId);
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
        [HttpPut("{dogId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutDog(int userId, int dogId, UpdateDog updateDog)
        {
            if (!UserExists(userId))
            {
                return NotFound("User not found");
            }

            var dog = await _context.Dogs.FindAsync(dogId);
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
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        /// <summary>
        /// Добавление собаки
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReadDog>> PostDog(int userId, UpdateDog newDog)
        {
            if (!UserExists(userId))
            {
                return NotFound("User not found");
            }

            var dog = new Dog
            {
                Name = newDog.Name,
                BirthDate = newDog.BirthDate,
                Breed = newDog.Breed,
                Weight = newDog.Weight,
                UserId = userId
            };

            try
            {
                _context.Dogs.Add(dog);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            var readDog = new ReadDog
            {
                Id = dog.Id,
                Name = dog.Name,
                BirthDate = dog.BirthDate,
                Breed = dog.Breed,
                Weight = dog.Weight
            };

            return Created("", readDog);
        }

        /// <summary>
        /// Удаление собаки
        /// </summary>
        [HttpDelete("{dogId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDog(int userId, int dogId)
        {
            if (!UserExists(userId))
            {
                return NotFound("User not found");
            }

            var dog = await _context.Dogs.FindAsync(dogId);
            if (dog == null)
            {
                return NotFound("Dog not found");
            }

            try
            {
                _context.Dogs.Remove(dog);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
