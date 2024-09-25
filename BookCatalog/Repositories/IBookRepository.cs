using System;
using BookCatalog.Entities;

namespace BookCatalog.Repositories;

public interface IBookRepository
{
    bool AuthorExists(Guid authorId);
    bool BookTitleExists(string bookTitle);
    void CreateBook(Book book);
    void DeleteBook(Guid id);
    bool Exist(Guid id);
    List<Book> GetAllBooks();
    Book? GetBook(Guid id);
    void Update(Book book);
}
