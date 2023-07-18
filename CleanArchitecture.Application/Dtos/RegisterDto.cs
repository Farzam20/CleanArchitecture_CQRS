using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.Dtos
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
