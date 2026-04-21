using backend.DTOs.Quotes;
using backend.Entities;

namespace backend.Mappings;

public static class QuoteMappings
{
    public static QuoteDto ToDto(this Quote quote)
    {
        return new QuoteDto
        {
            Id = quote.Id,
            Text = quote.Text,
            Author = quote.Author
        };
    }

    public static Quote ToEntity(this CreateQuoteDto dto, string userId)
    {
        return new Quote
        {
            Text = dto.Text,
            Author = dto.Author,
            UserId = userId
        };
    }

    public static void ApplyUpdate(this Quote quote, UpdateQuoteDto dto)
    {
        quote.Text = dto.Text;
        quote.Author = dto.Author;
    }
}