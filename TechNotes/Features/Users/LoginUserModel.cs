using System.ComponentModel.DataAnnotations;

namespace TechNotes.Features.Users;

public class LoginUserModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
