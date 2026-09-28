namespace TechNotes.Application.Notes.GetNoteById;

public class GetNoteByIdQuery : IQuery<NoteResponse?>
{
    public int Id { get; set; }
}
