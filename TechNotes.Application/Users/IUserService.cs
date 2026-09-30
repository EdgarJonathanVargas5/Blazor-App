namespace TechNotes.Application.Users;

public interface IUserService
{
    Task<string> GetCurrentUserIdAsync();
    Task<bool> IsCurrentUserInRoleAsync(string role);
    Task<bool> CurrentUserCanCreateNoteAsync();
    Task<bool> CurrentUserCanEditNoteAsync(int noteId);
    Task<List<string>> GetUserRolesAsync(string userId);
    Task AddUserRoleAsync(string userId, string roleName);
    Task RemoveRoleFromUserAsync(string userId, string roleName);
}
