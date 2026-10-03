namespace TechNotes.Application.Notes.TogglePublishNote;

public class TogglePublishNoteCommand : ICommand<NoteResponse>
{
    public int NoteId { get; set; }
}
