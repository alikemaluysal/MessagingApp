using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models;

public class ProfileSettingsViewModel
{
    public UpdateProfileInput Profile { get; set; } = new();
    public ChangePasswordInput Password { get; set; } = new();
}

public class UpdateProfileInput
{
    [Required]
    public Guid Id { get; set; }

    [Required, StringLength(50)]
    [Display(Name = "Kullanıcı Adı")]
    public string UserName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    [Display(Name = "Görünen Ad")]
    public string DisplayName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(200)]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Doğrulandı mı?")]
    public bool IsVerified { get; set; }

    // Sunucuda kayıtlı mevcut resmin URL'i (sadece gösterim için)
    public string? ExistingImageUrl { get; set; }

    [Display(Name = "Profil Resmi (jpg/png/gif)")]
    [DataType(DataType.Upload)]
    public IFormFile? ProfileImage { get; set; }
}

public class ChangePasswordInput
{
    [Required, DataType(DataType.Password)]
    [Display(Name = "Mevcut Şifre")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "{0} en az {2} karakter olmalı.")]
    [Display(Name = "Yeni Şifre")]
    public string NewPassword { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Yeni şifre ve tekrarı uyuşmuyor.")]
    [Display(Name = "Yeni Şifre (Tekrar)")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}