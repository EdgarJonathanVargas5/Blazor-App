using System.Net.Http.Json;
using TechNotes.Application.Notes;

namespace TechNotes.Client.Features.Notes;

public class NotesOverviewServiceClient : INoteOverviewService
{
    private readonly HttpClient _http;

    public NotesOverviewServiceClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<NoteResponse>?> GetNotesByCurrentUserAsync()
    {
        return await _http.GetFromJsonAsync<List<NoteResponse>>("/api/notes");
    }

    public async Task<NoteResponse?> TogglePublishNoteAsync(int noteId)
    {
        var result = await _http.PatchAsync($"/api/notes/{noteId}", null);
        if(result is not null && result.Content is not null)
        {
            return await result.Content.ReadFromJsonAsync<NoteResponse>();
        }
        return null;
    }
}
