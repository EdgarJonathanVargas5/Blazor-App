using TechNotes.Domain.Abstractions;

namespace TechNotes.Domain.Notes;

public class Note : Entity
{
    public required string Title { get; set; }
    public string? Content { get; set; }   
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; } = false;
    public string? UserId { get; set; }
}
