using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.Dtos
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
