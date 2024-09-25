using System;
using AutoMapper;
using BookCatalog.Entities;
using BookCatalog.Models.Dtos;
using BookCatalog.Repositories;

namespace BookCatalog.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookrepository;
    private readonly IMapper _mapper;
    public BookService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookrepository = bookRepository;
        _mapper = mapper;
    }

    public void CreateBook(BookDto bookDto)
    {
        Book book = _mapper.Map<Book>(bookDto);
        _bookrepository.CreateBook(book);
    }

    public void Delete(Guid id)
    {
        _bookrepository.DeleteBook(id);
    }

    public bool Exists(Guid id)
    {
        return _bookrepository.Exist(id);
    }

    public List<BookDto> GetAllBooks()
    {
        List<Book> books = _bookrepository.GetAllBooks();
        List<BookDto> bookDtos = books.Select(b => _mapper.Map<BookDto>(b)).ToList();

        return bookDtos;
    }

    public BookDto GetBook(Guid id)
    {
        Book? book = _bookrepository.GetBook(id);

        if (book == null)
            throw new ArgumentException("Book with this ID does not exists.");

        BookDto bookDto = _mapper.Map<BookDto>(book);

        return bookDto;
    }

    public void ValidateCreateDto(BookDto book)
    {
        try
        {
            if (book.Title.Length > 50)
                throw new ArgumentException("Maximum Title length is 50");

            if (book.AuthorId == Guid.Empty)
                throw new ArgumentException("AuthorId cannot be enpty Guid");

            if (!_bookrepository.AuthorExists(book.AuthorId))
                throw new ArgumentException("Author with this ID does not exists in database.");

            if (_bookrepository.BookTitleExists(book.Title))
                throw new ArgumentException("Book with this title already exists in database.");    
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException("Validation error: " + ex.Message, ex);
        }
    }

    public void Update(BookDto bookDto)
    {
        Book book = _mapper.Map<Book>(bookDto);
        _bookrepository.Update(book);
    }

    public void ValidateUpdateDto(BookDto book)
    {
                try
        {
            if (book.Title.Length > 50)
                throw new ArgumentException("Maximum Title length is 50");

            if (book.AuthorId == Guid.Empty)
                throw new ArgumentException("AuthorId cannot be enpty Guid");

            if (!_bookrepository.AuthorExists(book.AuthorId))
                throw new ArgumentException("Author with this ID does not exists in database.");  
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException("Validation error: " + ex.Message, ex);
        }
    }
}
