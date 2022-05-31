using DogsCompanion.App.Models.Account;
using DogsCompanion.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DogsCompanion.App.Controllers.Security
{
    [Route("api/account")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly DogsCompanionContext _context;

        public AuthorizationController(DogsCompanionContext context)
        {
            _context = context;
        }

        [HttpPost("auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthResponse>> Login(LoginInfo authInfo)
        {
            var user = await _context.Users.Where(u => u.Email == authInfo.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                return Unauthorized("Invalid Login or email");
            }

            if (!BCrypt.Net.BCrypt.Verify(authInfo.Password, user.Password))
            {
                return Unauthorized("Invalid Login or email");
            }

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                BirthDate = user.BirthDate
            };

            return Ok(authResponse);
        }
    }
}
