using backend.Data;
using backend.DTOs.Quotes;
using backend.Interfaces;
using backend.Mappings;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;
/// <summary>
/// Handles quote-related business logic and ensures users only access their own quotes.
/// </summary>
public class QuoteService : IQuoteService
{
    private readonly ApplicationDbContext _context;

    public QuoteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuoteDto>> GetAllAsync(string userId)
    {
        return await _context.Quotes
            .Where(q => q.UserId == userId)
            .OrderByDescending(q => q.Id)
            .Select(q => q.ToDto())
            .ToListAsync();
    }

    public async Task<QuoteDto?> GetByIdAsync(int id, string userId)
    {
        var quote = await _context.Quotes
            .FirstOrDefaultAsync(q => q.Id == id && q.UserId == userId);

        return quote?.ToDto();
    }

    public async Task<QuoteDto> CreateAsync(CreateQuoteDto dto, string userId)
    {
        var quote = dto.ToEntity(userId);

        _context.Quotes.Add(quote);
        await _context.SaveChangesAsync();

        return quote.ToDto();
    }

    public async Task<QuoteDto?> UpdateAsync(int id, UpdateQuoteDto dto, string userId)
    {
        var quote = await _context.Quotes
            .FirstOrDefaultAsync(q => q.Id == id && q.UserId == userId);

        if (quote is null) return null;

        quote.ApplyUpdate(dto);
        await _context.SaveChangesAsync();

        return quote.ToDto();
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var quote = await _context.Quotes
            .FirstOrDefaultAsync(q => q.Id == id && q.UserId == userId);

        if (quote is null) return false;

        _context.Quotes.Remove(quote);
        await _context.SaveChangesAsync();

        return true;
    }
}