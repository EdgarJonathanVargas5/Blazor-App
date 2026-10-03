using TechNotes.Application.Users;

namespace TechNotes.Application.Notes.TogglePublishNote;

public class TogglePublishNoteCommandHandler : ICommandHandler<TogglePublishNoteCommand, NoteResponse>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserService _userService;

    public TogglePublishNoteCommandHandler(INoteRepository noteRepository, IUserService userService)
    {
        _noteRepository = noteRepository;
        _userService = userService;
    }

    public async Task<Result<NoteResponse>> Handle(TogglePublishNoteCommand request, CancellationToken cancellationToken)
    {
        var currentUserCanEdit = await _userService.CurrentUserCanEditNoteAsync(request.NoteId);
        if(currentUserCanEdit is false)
        {
            return Result.Fail<NoteResponse>("No tienes permiso para editar esta nota");
        }
        var note = await _noteRepository.GetNoteByIdAsync(request.NoteId);
        if(note is null)
        {
            return Result.Fail<NoteResponse>("Nota no encontrada");
        }
        note.IsPublished = !note.IsPublished;
        note.UpdatedAt = DateTime.Now;
        if (note.IsPublished)
        {
            note.PublishedAt = DateTime.Now;
        }
        var updatedNote = await _noteRepository.UpdateNoteAsync(note);
        if(updatedNote is null)
        {
            return Result.Fail<NoteResponse>("No se pudo actualizar la nota");
        } 
        return updatedNote.Adapt<NoteResponse>();
    }
}
