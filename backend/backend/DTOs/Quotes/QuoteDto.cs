namespace backend.DTOs.Quotes;

public class QuoteDto
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
}