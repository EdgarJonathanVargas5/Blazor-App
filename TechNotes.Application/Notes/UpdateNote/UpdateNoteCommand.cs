namespace TechNotes.Application.Notes.UpdateNote;

public class UpdateNoteCommand : ICommand<NoteResponse?>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }   
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; } = false;
}
