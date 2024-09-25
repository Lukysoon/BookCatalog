using BookCatalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog;

// public class ApplicationDbContext : DbContext
// {
    // protected readonly IConfiguration _configuration;
    // public ApplicationDbContext(IConfiguration configuration)
    // {
    //     _configuration = configuration;
    // }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql(_configuration.GetConnectionString("BookContext"));
    // }

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

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}
