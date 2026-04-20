using backend.DTOs.Auth;
using backend.Entities;
using backend.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace backend.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userManager.FindByNameAsync(dto.UserName);
        if (existingUser is not null)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                Errors = ["Username already exists."]
            };
        }

        var existingEmail = await _userManager.FindByEmailAsync(dto.Email);
        if (existingEmail is not null)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                Errors = ["Email already exists."]
            };
        }

        var user = new AppUser
        {
            UserName = dto.UserName,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        var token = _tokenService.CreateToken(user);

        return new AuthResponseDto
        {
            IsSuccess = true,
            UserName = user.UserName ?? string.Empty,
            Token = token
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);

        if (user is null)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                Errors = ["Invalid username or password."]
            };
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!passwordValid)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                Errors = ["Invalid username or password."]
            };
        }

        var token = _tokenService.CreateToken(user);

        return new AuthResponseDto
        {
            IsSuccess = true,
            UserName = user.UserName ?? string.Empty,
            Token = token
        };
    }
}