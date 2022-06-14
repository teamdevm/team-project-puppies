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
using Microsoft.AspNetCore.Authorization;
using DogsCompanion.App.Authentication;
using DogsCompanion.App.Models.Personal;

namespace DogsCompanion.App.Controllers.Personal
{
    [Authorize]
    [ApiController]
    [Route("api/account/me")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PersonalController : ControllerBase
    {
        private readonly DogsCompanionContext _context;
        private readonly int? _userId;
        public PersonalController(DogsCompanionContext context, ClaimsValidationService claimsValidationService)
        {
            _context = context;
            _userId = claimsValidationService.GetUserId();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserInfo>> GetUserInfo()
        {
            if (_userId == null)
            {
                return Unauthorized("No JTI claim");
            }
            int userId = _userId!.Value;

            var user = await _context.Users.FindAsync(userId);
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
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUser(UpdateUser updateUser)
        {
            if (_userId == null)
            {
                return Unauthorized("No JTI claim");
            }
            int userId = _userId!.Value;

            var user = await _context.Users.FindAsync(userId);
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
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }

            return NoContent();
        }
    }
}
