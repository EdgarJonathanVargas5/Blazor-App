using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TechNotes.Application.Exceptions;
using TechNotes.Application.Users;
using TechNotes.Domain.Notes;

namespace TechNotes.Infrastructure.Users;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly INoteRepository _noteRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<User> userManager, INoteRepository noteRepository, 
                       IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _noteRepository = noteRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> CurrentUserCanCreateNoteAsync()
    {
        var user = await GetCurrentUserAsync();
        if(user is null)
        {
            return false;
        }
        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        var isWriter = await _userManager.IsInRoleAsync(user, "Writer");
        return isAdmin || isWriter;
    }

    public async Task<bool> CurrentUserCanEditNoteAsync(int noteId)
    {
        var user = await GetCurrentUserAsync();
        if(user is null)
        {
            return false;
        }
        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        var isWriter = await _userManager.IsInRoleAsync(user, "Writer");
        var note = await _noteRepository.GetNoteByIdAsync(noteId);

        if(note is null)
        {
            return false;
        }
        var isAuthorized = isAdmin || (isWriter && note.UserId == user.Id);
        return isAuthorized;
    }

    public async Task<string> GetCurrentUserIdAsync()
    {
        var user = await GetCurrentUserAsync();
        if(user is null)
        {
            throw new UserNotAuthorizedException();
        }
        return user.Id;
    }

    public async Task<bool> IsCurrentUserInRoleAsync(string role)
    {
        var user = await GetCurrentUserAsync();
        var isUserInRole = user is not null && await _userManager.IsInRoleAsync(user, role);
        return isUserInRole;
    }

    private async Task<User?> GetCurrentUserAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if(httpContext is null || httpContext.User is null)
        {
            return null;
        }
        var user = await _userManager.GetUserAsync(httpContext.User);
        return user;
    }
}
