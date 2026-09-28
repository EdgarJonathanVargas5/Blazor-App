using Microsoft.EntityFrameworkCore;
using TechNotes.Domain.Notes;

namespace TechNotes.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly ApplicationDbContext _context;
    
    public NoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Note>> GetAllNotesAsync()
    {
        return await _context.Notes.OrderBy(n => n.Id).ToListAsync();
    }

    public async Task<Note> CreateNoteAsync(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return note;
    }

    public async Task<Note?> GetNoteByIdAsync(int id)
    {
        return await _context.Notes.FindAsync(id);
    }

    public async Task<Note?> UpdateNoteAsync(Note note)
    {
        var noteToUpdate = await GetNoteByIdAsync(note.Id);
        if(noteToUpdate is null)
        {
            return null;
        }
        noteToUpdate.Title = note.Title;
        noteToUpdate.Content = note.Content;
        noteToUpdate.PublishedAt = note.PublishedAt;
        noteToUpdate.IsPublished = note.IsPublished;
        noteToUpdate.UpdatedAt = DateTime.Now;
        _context.Notes.Update(noteToUpdate);
        await _context.SaveChangesAsync();
        return noteToUpdate;
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        var noteToDelete = await GetNoteByIdAsync(id);
        if(noteToDelete is null)
        {
            return false;
        }
        _context.Notes.Remove(noteToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}
