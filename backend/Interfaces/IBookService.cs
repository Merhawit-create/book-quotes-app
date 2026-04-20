using backend.DTOs.Books;

namespace backend.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync(string userId);
    Task<BookDto?> GetByIdAsync(int id, string userId);
    Task<BookDto> CreateAsync(CreateBookDto dto, string userId);
    Task<BookDto?> UpdateAsync(int id, UpdateBookDto dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}