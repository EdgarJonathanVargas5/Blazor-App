namespace TechNotes.Features.Notes.Services;

public interface INoteColorService
{
    string GetNoteBackgroundColor(int noteId);
    string GetNoteBorderColor(int noteId);
    string GetNoteInLineStyle(int noteId);
}
