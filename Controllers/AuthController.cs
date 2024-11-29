using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LocalRecipes.Data;
using LocalRecipes.Models;
using LocalRecipes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LocalRecipes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly DataContext context;
        private readonly TokenService tokenService;

        public AuthController(DataContext context, TokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }
        
          [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                return BadRequest(new { Message = "User with that email is already registered!" });
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedAt = DateTime.Now
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully!" });
        }

        // Utility method to hash the password
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        //utility method to verify passwords during logins
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
{
    using (var hmac = new HMACSHA512(storedSalt))
    {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(storedHash);
    }
}

}
}