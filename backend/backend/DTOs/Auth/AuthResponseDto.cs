namespace backend.DTOs.Auth;

public class AuthResponseDto
{
    public bool IsSuccess { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = [];
}