using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using backend.Entities;

namespace backend.Data;
/// <summary>
/// Entity Framework database context for Identity users, books, and quotes.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Quote> Quotes => Set<Quote>();
}