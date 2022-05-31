using BCrypt.Net;
using DogsCompanion.App.Models;
using DogsCompanion.App.Models.Account;
using DogsCompanion.Data;
using DogsCompanion.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DogsCompanionContext _context;

        public AccountController(DogsCompanionContext context)
        {
            _context = context;
        }

        [HttpPost("register-user")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserInfo>> RegisterUser(RegisterInfo registerInfo)
        {
            var user = await _context.Users
                .Where(u => u.Email == registerInfo.Email || u.PhoneNumber == registerInfo.PhoneNumber)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                return Conflict("Email or phone number already in use");
            }

            var newUser = new User
            {
                Email = registerInfo.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerInfo.Password),
                PhoneNumber = registerInfo.PhoneNumber,
                FirstName = registerInfo.FirstName,
                LastName = registerInfo.LastName,
                MiddleName = registerInfo.MiddleName,
                BirthDate = registerInfo.BirthDate,
            };

            try
            {
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            var userInfo = new UserInfo
            {
                Id = newUser.Id,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                MiddleName = newUser.MiddleName,
                BirthDate = newUser.BirthDate
            };

            return Created("", userInfo);
        }
    }
}
