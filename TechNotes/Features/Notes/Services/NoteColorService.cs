namespace TechNotes.Features.Notes.Services;

public class NoteColorService : INoteColorService
{
    private string[] _noteBackgroundColors =
    [
        "#fefce8", // Warm yellow
        "#f0f9ff", // Cool blue  
        "#f0fdf4", // Fresh green
        "#fdf2f8", // Soft pink
        "#f5f3ff", // Light purple
        "#fff7ed", // Warm orange
        "#ecfdf5", // Mint green
        "#fef3c7", // Golden yellow
        "#dbeafe", // Sky blue
        "#e0e7ff"  // Lavender
    ];
    
    private string[] _noteBorderColors =
    [
        "#fbbf24", // Golden
        "#3b82f6", // Blue
        "#10b981", // Emerald
        "#ec4899", // Pink
        "#8b5cf6", // Purple
        "#f97316", // Orange
        "#06b6d4", // Cyan
        "#eab308", // Yellow
        "#6366f1", // Indigo
        "#84cc16"  // Lime
    ];

    public string GetNoteBackgroundColor(int noteId)
    {
        var backgroundColorIndex = noteId % _noteBackgroundColors.Length;
        return _noteBackgroundColors[backgroundColorIndex];
    }

    public string GetNoteBorderColor(int noteId)
    {
        var noteBorderColorIndex = noteId % _noteBorderColors.Length;
        return _noteBorderColors[noteBorderColorIndex];
    }

    public string GetNoteInLineStyle(int noteId)
    {
        var backgroundColor = GetNoteBackgroundColor(noteId);
        var borderColor = GetNoteBorderColor(noteId);
        return $"background-color: {backgroundColor}; border-left-color: {borderColor};";
    }
}
