using backend.DTOs.Auth;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
/// <summary>
/// Handles authentication-related API endpoints such as user registration and login.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    /// <summary>
    /// Initializes a new instance of the AuthController.
    /// </summary>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    /// <summary>
    /// Registers a new user account and returns a JWT token when successful.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    /// <summary>
    /// Authenticates an existing user and returns a JWT token when credentials are valid.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (!result.IsSuccess)
            return Unauthorized(result);

        return Ok(result);
    }
}