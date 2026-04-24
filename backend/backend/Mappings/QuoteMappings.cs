using backend.DTOs.Quotes;
using backend.Entities;

namespace backend.Mappings;
/// <summary>
/// Provides mapping helper methods between Quote entities and DTOs.
/// </summary>
public static class QuoteMappings
{ 
    /// <summary>
    /// Converts a Quote entity to a QuoteDto for API responses.
    /// </summary>
    public static QuoteDto ToDto(this Quote quote)
    {
        return new QuoteDto
        {
            Id = quote.Id,
            Text = quote.Text,
            Author = quote.Author,
            IsFavorite = quote.IsFavorite
        };
    }
    /// <summary>
    /// Creates a new Quote entity from a CreateQuoteDto and assigns it to a user.
    /// </summary>
    public static Quote ToEntity(this CreateQuoteDto dto, string userId)
    {
        return new Quote
        {
            Text = dto.Text,
            Author = dto.Author,
            IsFavorite = dto.IsFavorite,
            UserId = userId
        };
    }
    /// <summary>
    /// Applies updated values from an UpdateQuoteDto to an existing Quote entity.
    /// </summary>
    public static void ApplyUpdate(this Quote quote, UpdateQuoteDto dto)
    {
        quote.Text = dto.Text;
        quote.Author = dto.Author;
        quote.IsFavorite = dto.IsFavorite;
    }
}