using BCrypt.Net;
        
namespace TechLaundry.Services
{
    public class PasswordService
    {  

        public string HashPassword(string Password)
        {
            return BCrypt.Net.BCrypt.HashPassword(Password);
        }
        public bool VerifyPassword(string Password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(Password, hash);
        }
    }
}
