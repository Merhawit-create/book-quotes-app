using System.Security.Claims;
using backend.DTOs.Quotes;
using backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuotesController : ControllerBase
{
    private readonly IQuoteService _quoteService;

    public QuotesController(IQuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var quotes = await _quoteService.GetAllAsync(userId);
        return Ok(quotes);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var quote = await _quoteService.GetByIdAsync(id, userId);
        if (quote is null) return NotFound();

        return Ok(quote);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateQuoteDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var createdQuote = await _quoteService.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = createdQuote.Id }, createdQuote);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateQuoteDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var updatedQuote = await _quoteService.UpdateAsync(id, dto, userId);
        if (updatedQuote is null) return NotFound();

        return Ok(updatedQuote);
    }

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