using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models;

public class VerifyViewModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required, MinLength(6), MaxLength(6)]
    public string VerificationCode { get; set; } = null!;
}