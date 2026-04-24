using backend.Data;
using backend.DTOs.Books;
using backend.Interfaces;
using backend.Mappings;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;
/// <summary>
/// Handles book-related business logic and ensures users only access their own books.
/// </summary>
public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync(string userId)
    {
        return await _context.Books
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.Id)
            .Select(b => b.ToDto())
            .ToListAsync();
    }

    public async Task<BookDto?> GetByIdAsync(int id, string userId)
    {
        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

        return book?.ToDto();
    }

    public async Task<BookDto> CreateAsync(CreateBookDto dto, string userId)
    {
        var book = dto.ToEntity(userId);

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return book.ToDto();
    }

    public async Task<BookDto?> UpdateAsync(int id, UpdateBookDto dto, string userId)
    {
        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

        if (book is null) return null;
        

        book.ApplyUpdate(dto);
        await _context.SaveChangesAsync();

        return book.ToDto();
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

        if (book is null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return true;
    }
}