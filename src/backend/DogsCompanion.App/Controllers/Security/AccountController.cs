using BCrypt.Net;
using DogsCompanion.App.Authentication;
using DogsCompanion.App.Models;
using DogsCompanion.App.Models.Account;
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

namespace DogsCompanion.App.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DogsCompanionContext _context;
        private readonly JwtManager _jwtManager;
        private readonly int? _userId;

        public AccountController(DogsCompanionContext context, 
            ClaimsValidationService claimsValidationService,
            JwtManager jwtManager)
        {
            _context = context;
            _userId = claimsValidationService.GetUserId();
            _jwtManager = jwtManager;
        }

        [AllowAnonymous]
        [HttpPost("register-user")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RegisterResponse>> RegisterUser(RegisterInfo registerInfo)
        {
            var registerResponse = new RegisterResponse();

            registerInfo.Email = registerInfo.Email.ToLower();

            registerResponse.IsEmailInUse = await _context.Users.AnyAsync(u => u.Email == registerInfo.Email);
            registerResponse.IsPhoneInUse = await _context.Users.AnyAsync(u => u.PhoneNumber == registerInfo.PhoneNumber);

            if (registerResponse.IsEmailInUse || registerResponse.IsPhoneInUse)
            {
                return Conflict(registerResponse);
            }

            var user = new User
            {
                Email = registerInfo.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerInfo.Password),
                PhoneNumber = registerInfo.PhoneNumber,
                FirstName = registerInfo.FirstName,
                LastName = registerInfo.LastName,
                MiddleName = registerInfo.MiddleName,
                BirthDate = registerInfo.BirthDate,
            };

            var dog = new Dog
            {
                Name = registerInfo.Dog.Name,
                BirthDate = registerInfo.Dog.BirthDate,
                Breed = registerInfo.Dog.Breed,
                Weight = registerInfo.Dog.Weight,
                User = user,
            };

            try
            {
                _context.Users.Add(user);
                _context.Dogs.Add(dog);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }

            // Генерация токенов для нового пользователя
            Tokens tokens;
            try
            {
                tokens = _jwtManager.GenerateTokens(user.Id.ToString());

                var dbRefreshToken = new RefreshToken()
                {
                    JwtId = tokens.Jti,
                    IsUsed = false,
                    UserId = user.Id,
                    AddedDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddDays(30),
                    IsRevoked = false,
                    Token = tokens.RefreshToken,
                };

                await _context.RefreshTokens.AddAsync(dbRefreshToken);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException updateExc)
            {
                // TODO log: DbUpdateException
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }
            catch (Exception exc)
            {
                //TODO log: Token generation error
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }

            var userInfo = new UserInfo
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                BirthDate = user.BirthDate
            };

            var dogInfo = new ReadDog
            {
                Id = dog.Id,
                Name = dog.Name,
                BirthDate = dog.BirthDate,
                Breed = dog.Breed,
                Weight = dog.Weight,
                UserId = user.Id,
            };

            registerResponse.UserInfo = userInfo;
            registerResponse.DogInfo = dogInfo;
            registerResponse.AccessToken = tokens.AccessToken;
            registerResponse.RefreshToken = tokens.RefreshToken;

            return Created("", registerResponse);
        }

        /// <summary>
        /// Обновление почты
        /// </summary>
        [Authorize]
        [HttpPut("change-email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ChangeEmail(ChangeEmailRequest request)
        {
            if (_userId == null)
            {
                return Unauthorized("No JTI claim");
            }
            int userId = _userId!.Value;

            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized("Invalid Login or email");
            }

            bool isNewEmailUsed = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (isNewEmailUsed)
            {
                return Conflict("Email already in use");
            }

            try
            {
                user.Email = request.Email;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }

            return NoContent();
        }

        /// <summary>
        /// Обновление пароля
        /// </summary>
        [Authorize]
        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            if (_userId == null)
            {
                return Unauthorized("No JTI claim");
            }
            int userId = _userId!.Value;

            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
            {
                return Unauthorized("Invalid Login or email");
            }

            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }

            return NoContent();
        }

        /// <summary>
        /// Обновление номера телефона
        /// </summary>
        [Authorize]
        [HttpPut("change-phone")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePhone(ChangePhoneRequest request)
        {
            if (_userId == null)
            {
                return Unauthorized("No JTI claim");
            }
            int userId = _userId!.Value;

            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized("Invalid Login or email");
            }

            bool isNewPhoneUsed = await _context.Users.AnyAsync(u => u.PhoneNumber == request.NewPhoneNumber);
            if (isNewPhoneUsed)
            {
                return Conflict("Phone already in use");
            }

            try
            {
                user.PhoneNumber = request.NewPhoneNumber;
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
