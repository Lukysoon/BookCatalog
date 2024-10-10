using BookCatalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            assembly: typeof(ApplicationDbContext).Assembly
        );
    }

    public DbSet<Book> Books { get; set; }
}
