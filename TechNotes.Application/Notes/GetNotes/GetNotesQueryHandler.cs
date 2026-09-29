using TechNotes.Application.Users;
using TechNotes.Domain.User;

namespace TechNotes.Application.Notes.GetNotes;

public class GetNotesQueryHandler : IQueryHandler<GetNotesQuery, List<NoteResponse>>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public GetNotesQueryHandler(INoteRepository noteRepository, IUserRepository userRepository, 
                                IUserService userService)
    {
        _noteRepository = noteRepository;
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<Result<List<NoteResponse>>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _noteRepository.GetAllNotesAsync();
        var response = new List<NoteResponse>();
        foreach(var note in notes)
        {
            var noteResponse = note.Adapt<NoteResponse>();
            if(note.UserId != null)
            {
                var noteAuthor = await _userRepository.GetUserByIdAsync(note.UserId);
                noteResponse.UserName = noteAuthor?.UserName ?? "Desconocido";
                noteResponse.UserId = note.UserId;
                noteResponse.CanEdit = await _userService.CurrentUserCanEditNoteAsync(note.Id);
            }
            else
            {
                noteResponse.UserName = "Desconocido";
            }
            response.Add(noteResponse);
        }
        return response.OrderByDescending(n => n.PublishedAt).ToList();
    }
}
