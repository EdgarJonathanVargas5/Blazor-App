using TechNotes.Application.Users;

namespace TechNotes.Application.Notes.GetNotesByCurrentUser;

public class GetNotesByCurrentUserQueryHandler : IQueryHandler<GetNotesByCurrentUserQuery, List<NoteResponse>>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserService _userService;

    public GetNotesByCurrentUserQueryHandler(INoteRepository noteRepository, IUserService userService)
    {
        _noteRepository = noteRepository;
        _userService = userService;
    }

    public async Task<Result<List<NoteResponse>>> Handle(GetNotesByCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetCurrentUserIdAsync();
        var notes = await _noteRepository.GetAllNotesByUserAsync(userId);
        var result = notes.Adapt<List<NoteResponse>>();
        return result.OrderByDescending(n => n.PublishedAt).ToList();
    }
}
