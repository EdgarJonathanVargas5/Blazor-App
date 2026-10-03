namespace TechNotes.Application.Notes;

public interface INoteOverviewService
{
    Task<NoteResponse?> TogglePublishNoteAsync(int noteId);
    Task<List<NoteResponse>?> GetNotesByCurrentUserAsync();
}
