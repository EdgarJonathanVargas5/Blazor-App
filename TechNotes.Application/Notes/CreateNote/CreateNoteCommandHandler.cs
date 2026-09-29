using TechNotes.Application.Exceptions;
using TechNotes.Application.Users;

namespace TechNotes.Application.Notes.CreateNote;

public class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommand, NoteResponse>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserService _userService;

    public CreateNoteCommandHandler(INoteRepository noteRepository, IUserService userService)
    {
        _noteRepository = noteRepository;
        _userService = userService;
    }

    public async Task<Result<NoteResponse>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newNote = request.Adapt<Note>();
            var userId = await _userService.GetCurrentUserIdAsync();
            if(userId is null)
            {
                return FailNoteCreate();
            }
            var isCurrentUserCanCreate = await _userService.CurrentUserCanCreateNoteAsync();
            if (isCurrentUserCanCreate is false)
            {
                return FailNoteCreate();
            }
            newNote.UserId = userId;
            var note = await _noteRepository.CreateNoteAsync(newNote);
            return note.Adapt<NoteResponse>();
        }
        catch (UserNotAuthorizedException)
        {
           return FailNoteCreate();
        }
        
    }

    private static Result<NoteResponse> FailNoteCreate()
    {
        return Result.Fail<NoteResponse>("El usuario no esta autorizado para crear una nota");
    }
}
