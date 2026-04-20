using backend.DTOs.Quotes;

namespace backend.Interfaces;

public interface IQuoteService
{
    Task<IEnumerable<QuoteDto>> GetAllAsync(string userId);
    Task<QuoteDto?> GetByIdAsync(int id, string userId);
    Task<QuoteDto> CreateAsync(CreateQuoteDto dto, string userId);
    Task<QuoteDto?> UpdateAsync(int id, UpdateQuoteDto dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}