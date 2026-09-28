namespace TechNotes.Components.Pages;

public class NoteModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }   
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; } = false;
    public string? UserName { get; set; }
}
