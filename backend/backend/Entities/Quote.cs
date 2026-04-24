namespace backend.Entities;

public class Quote
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public bool IsFavorite { get; set; } = false; 
    public string UserId { get; set; } = string.Empty;
    public AppUser? User { get; set; }
}