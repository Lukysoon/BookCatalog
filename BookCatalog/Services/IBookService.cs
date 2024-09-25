using System;
using BookCatalog.Models.Dtos;

namespace BookCatalog.Services;

public interface IBookService
{
    void CreateBook(BookDto book);
    void Delete(Guid id);
    bool Exists(Guid id);
    List<BookDto> GetAllBooks();
    BookDto GetBook(Guid id);
    void ValidateCreateDto(BookDto book);
    void Update(BookDto book);
    void ValidateUpdateDto(BookDto book);
}
