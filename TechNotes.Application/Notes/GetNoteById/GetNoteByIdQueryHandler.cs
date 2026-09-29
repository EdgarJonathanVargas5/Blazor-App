using TechNotes.Application.Users;

namespace TechNotes.Application.Notes.GetNoteById;

public class GetNoteByIdQueryHandler : IQueryHandler<GetNoteByIdQuery, NoteResponse?>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    
    public GetNoteByIdQueryHandler(INoteRepository noteRepository, IUserRepository userRepository, 
                                   IUserService userService)
    {
        _noteRepository = noteRepository;
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<Result<NoteResponse?>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetNoteByIdAsync(request.Id);
        if(note is null)
        {
            return Result.Fail<NoteResponse?>("Nota no encontrada");
        }
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
        return noteResponse;
    }
}
