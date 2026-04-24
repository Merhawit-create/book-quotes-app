using System.Security.Claims;
using backend.DTOs.Books;
using backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
/// <summary>
/// Provides authenticated CRUD operations for books owned by the current user.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }
    /// <summary>
    /// Returns all books that belong to the authenticated user.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var books = await _bookService.GetAllAsync(userId);
        return Ok(books);
    }
    /// <summary>
    /// Returns a single book by id if it belongs to the authenticated user.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var book = await _bookService.GetByIdAsync(id, userId);
        if (book is null) return NotFound();

        return Ok(book);
    }
    /// <summary>
    /// Creates a new book for the authenticated user.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var createdBook = await _bookService.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
    }
    /// <summary>
    /// Updates an existing book if it belongs to the authenticated user.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateBookDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var updatedBook = await _bookService.UpdateAsync(id, dto, userId);
        if (updatedBook is null) return NotFound();

        return Ok(updatedBook);
    }
    /// <summary>
    /// Deletes a book if it belongs to the authenticated user.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var deleted = await _bookService.DeleteAsync(id, userId);
        if (!deleted) return NotFound();

        return NoContent();
    }
}