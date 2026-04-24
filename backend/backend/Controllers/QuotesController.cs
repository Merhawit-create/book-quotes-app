using System.Security.Claims;
using backend.DTOs.Quotes;
using backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
/// <summary>
/// Provides authenticated CRUD operations for quotes owned by the current user.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]

public class QuotesController : ControllerBase
{
    private readonly IQuoteService _quoteService;
    /// <summary>
    /// Initializes a new instance of the QuotesController.
    /// </summary>
    public QuotesController(IQuoteService quoteService)
    {
        _quoteService = quoteService;
    }
    /// <summary>
    /// Returns all quotes that belong to the authenticated user.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var quotes = await _quoteService.GetAllAsync(userId);
        return Ok(quotes);
    }
    /// <summary>
    /// Returns a specific quote by ID if it belongs to the authenticated user.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var quote = await _quoteService.GetByIdAsync(id, userId);
        if (quote is null) return NotFound();

        return Ok(quote);
    }
    /// <summary>
    /// Creates a new quote for the authenticated user.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateQuoteDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var createdQuote = await _quoteService.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = createdQuote.Id }, createdQuote);
    }
    /// <summary>
    /// Updates an existing quote if it belongs to the authenticated user.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateQuoteDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var updatedQuote = await _quoteService.UpdateAsync(id, dto, userId);
        if (updatedQuote is null) return NotFound();

        return Ok(updatedQuote);
    }
    /// <summary>
    /// Deletes a quote if it belongs to the authenticated user.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var deleted = await _quoteService.DeleteAsync(id, userId);
        if (!deleted) return NotFound();

        return NoContent();
    }
}