using System.ComponentModel.DataAnnotations;

namespace TechNotes.Features.Users;

public class RegisterUserModel
{
    [Required(ErrorMessage = "Nombre de usuario requerido")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email requerido")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contraseña requerida")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirmacion de contraseña requerido")]
    [Compare("Password", ErrorMessage = "La contraseña y la confirmacion de contraseña no coinciden")]
    public string ConfirmPassword { get; set; } = string.Empty;

}
