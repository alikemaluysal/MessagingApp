using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MinLength(4), DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}