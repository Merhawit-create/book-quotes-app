namespace backend.DTOs.Quotes;

public class CreateQuoteDto
{
    public string Text { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public bool IsFavorite { get; set; } = false;
}