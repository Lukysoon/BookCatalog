using System;
using System.Formats.Tar;
using BookCatalog.Entities;

namespace BookCatalog.Repositories;

public class BookRepository : IBookRepository
{
   private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool AuthorExists(Guid authorId)
    {
        bool authorExists = _context.Authors.Any(a => a.Id == authorId);
        return authorExists;
    }

    public bool BookTitleExists(string bookTitle)
    {
        bool titleExists = _context.Books.Any(b => b.Title == bookTitle);
        return titleExists;
    }

    public void CreateBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void DeleteBook(Guid id)
    {
        Book book = _context.Books.Find(id)!;

        _context.Books.Remove(book);
        _context.SaveChanges();
    }

    public bool Exist(Guid id)
    {
        return _context.Books.Any(b => b.Id == id);
    }

    public List<Book> GetAllBooks()
    {
        List<Book> books = _context.Books.ToList();
        return books;
    }

    public Book? GetBook(Guid id)
    {
        Book? book = _context.Books.Where(b => b.Id == id).FirstOrDefault();
        return book;
    }

    public void Update(Book book)
    {
        _context.Books.Update(book);
        _context.SaveChanges();
    }


}