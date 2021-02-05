using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.Acefb9Utils;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZPI_Database.DataAccess;
using ZPI_Database.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ZPIContext _context;

        public AccountController(IConfiguration config, ZPIContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post(UserLogin _userData)
        {

            if (ModelState.IsValid)
            {
                var user = await GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Surname", user.Surname),
                    new Claim("Email", user.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], 
                        claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    var finalToken = new JwtSecurityTokenHandler().WriteToken(token);

                    user.Token = finalToken;

                    await _context.SaveChangesAsync();

                    return Ok(finalToken);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            if (user.AccountType != AccountType.stud.ToString() &&
                user.AccountType != AccountType.work.ToString())
            {
                ModelState.AddModelError("", "Błąd przy wyborze rodzaju konta");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            if (_context.Users.Any(x => x.Email == user.Email))
            {
                ModelState.AddModelError("", "Powtarzający się email");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Register", new { id = user.Id }, user);
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task Logout()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var user = await _context.Users.FirstOrDefaultAsync(u => "Bearer "+u.Token == authHeader);

            user.Token = null;

            await _context.SaveChangesAsync();
        }

        private async Task<User> GetUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user != null)
                return user;
            return null;
        }
    }
}
