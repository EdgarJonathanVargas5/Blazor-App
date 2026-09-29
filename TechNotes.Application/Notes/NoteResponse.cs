namespace TechNotes.Application.Notes;

public record struct NoteResponse(
    int Id,
    string Title,
    string? Content,
    DateTime CreatedAt,
    DateTime PublishedAt,
    bool IsPublished,
    string? UserName,
    string UserId,
    bool CanEdit
);