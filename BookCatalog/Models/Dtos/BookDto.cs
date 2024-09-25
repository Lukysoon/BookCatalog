using System;

namespace BookCatalog.Models.Dtos;

public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Guid AuthorId { get; set; }
}
