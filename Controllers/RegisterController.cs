using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLaundry.Data;
using TechLaundry.DTOs;
using TechLaundry.Models;
using TechLaundry.Services;

namespace TechLaundry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;

        public RegisterController(AppDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterDto dto)

        {
            if (dto.Email[0] < 'a') return BadRequest("Set Email Correctly");
            var existinguser = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existinguser != null)
            {
                return BadRequest("Email Already Exist");
            }
            
            var hashPassword = _passwordService.HashPassword(dto.Password);
            Console.WriteLine("This is hash", hashPassword);
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash=hashPassword,
                Role=dto.Role
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new {
                massage = "User Registered Successfully",
                user.Id,
                user.Email
                
            });

        }
    }
}