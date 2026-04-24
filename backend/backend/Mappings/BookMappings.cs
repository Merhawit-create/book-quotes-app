using backend.DTOs.Books;
using backend.Entities;

namespace backend.Mappings;
/// <summary>
/// Provides mapping helpers between Book entities and Book DTOs.
/// </summary>
public static class BookMappings
{
    /// <summary>
    /// Converts a Book entity to a BookDto used by the API response.
    /// </summary>
    public static BookDto ToDto(this Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            PublishedDate = book.PublishedDate
        };
    }
    /// <summary>
    /// Creates a Book entity from a CreateBookDto and assigns it to a user.
    /// </summary>
    public static Book ToEntity(this CreateBookDto dto, string userId)
    {
        return new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            PublishedDate = dto.PublishedDate,
            UserId = userId
        };
    }
    /// <summary>
    /// Applies updated values from an UpdateBookDto to an existing Book entity.
    /// </summary>
    public static void ApplyUpdate(this Book book, UpdateBookDto dto)
    {
        if (dto.Title != null)
            book.Title = dto.Title;

        if (dto.Author != null)
            book.Author = dto.Author;

        if (dto.PublishedDate.HasValue)
            book.PublishedDate = dto.PublishedDate.Value;
    }
}