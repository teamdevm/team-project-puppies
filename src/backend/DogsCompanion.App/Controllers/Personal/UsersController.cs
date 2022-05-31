using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DogsCompanion.Data;
using DogsCompanion.Data.Entities;
using DogsCompanion.App.Models;
using DogsCompanion.App.Models.Account;
using DogsCompanion.App.Models.Account.Update;

namespace DogsCompanion.App.Controllers.Personal
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DogsCompanionContext _context;

        public UsersController(DogsCompanionContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInfo>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserInfo userInfo = new UserInfo
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                BirthDate = user.BirthDate,
            };

            return Ok(userInfo);
        }

        /// <summary>
        /// Обновление информации о пользователе, не требующей подтверждения
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUser(int id, UpdateUser updateUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;
            user.MiddleName = updateUser.MiddleName;
            user.BirthDate = updateUser.BirthDate;

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
        /// Обновление почты
        /// </summary>
        [HttpPut("{id}/email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutUserEmail(int id, UpdateEmail updateEmail)
        {
            var updateUser = await _context.Users.FindAsync(id);
            if (updateUser == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Where(u => u.Email == updateEmail.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                return Conflict("Email already in use");
            }

            // TODO подтверждение смены почты
            updateUser.Email = updateEmail.Email;

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
        /// Обновление пароля
        /// </summary>
        [HttpPut("{id}/password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUserPassword(int id, UpdatePassword updatePassword)
        {
            var updateUser = await _context.Users.FindAsync(id);
            if (updateUser == null)
            {
                return NotFound();
            }

            updateUser.Password = BCrypt.Net.BCrypt.HashPassword(updatePassword.Password);

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
        /// Обновление номера телефона
        /// </summary>
        [HttpPut("{id}/phone")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PutUserPhone(int id, UpdatePhone updatePhone)
        {
            var updateUser = await _context.Users.FindAsync(id);
            if (updateUser == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Where(u => u.PhoneNumber == updatePhone.PhoneNumber).FirstOrDefaultAsync();
            if (user != null)
            {
                return Conflict("Phone already in use");
            }

            updateUser.PhoneNumber = updatePhone.PhoneNumber;

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
    }
}
