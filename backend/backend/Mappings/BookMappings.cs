using backend.DTOs.Books;
using backend.Entities;

namespace backend.Mappings;

public static class BookMappings
{
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

    public static void ApplyUpdate(this Book book, UpdateBookDto dto)
    {
        book.Title = dto.Title;
        book.Author = dto.Author;
        book.PublishedDate = dto.PublishedDate;
    }
}