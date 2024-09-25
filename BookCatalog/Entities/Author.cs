using System;

namespace BookCatalog.Entities;

public class Author
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public List<Book> Books { get; set; } = new List<Book>();
}
