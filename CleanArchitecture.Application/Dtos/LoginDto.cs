using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.Dtos
{
    public class LoginDto
    {
        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
