namespace TechNotes.Application.Users;

public interface IUserService
{
    Task<string> GetCurrentUserIdAsync();
    Task<bool> IsCurrentUserInRoleAsync(string role);
    Task<bool> CurrentUserCanCreateNoteAsync();
    Task<bool> CurrentUserCanEditNoteAsync(int noteId);
}
