using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLaundry.Services;
using TechLaundry.Data;
using TechLaundry.DTOs;

namespace TechLaundry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly TokenService _tokenService;
        public LoginController(AppDbContext context, PasswordService passwordService, TokenService tokenService)
        {
            _context = context;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user == null)
            {
                return BadRequest("Invalid Email or Password");
            }
            var isPasswordValid = _passwordService.VerifyPassword(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return BadRequest("Invalid Email or Password");
            }
            var token = _tokenService.GenerateToken(user.Email, user.Role);
            
            return Ok(new
            {
                message = "Login Successful",
                token,
                user.Id,
                user.Email,
                user.Role
            });
        }

    }
}
