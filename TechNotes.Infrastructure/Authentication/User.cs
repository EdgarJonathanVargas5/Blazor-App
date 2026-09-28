using Microsoft.AspNetCore.Identity;
using TechNotes.Domain.Notes;
using TechNotes.Domain.User;

namespace TechNotes.Infrastructure.Authentication;

public class User : IdentityUser, IUser
{
    public List<Note> Notes { get; set; } = new List<Note>();
}
