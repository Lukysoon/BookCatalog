using AutoMapper;
using BookCatalog.Entities;
using BookCatalog.Models.Dtos;
using BookCatalog.Repositories;
using BookCatalog.Services;
using Moq;

namespace BookCatalog.Tests;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _mockMapper = new Mock<IMapper>();
            _bookService = new BookService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetAllBooks_ReturnsCorrectNumberOfBooks()
        {
            Guid bookId1 = Guid.NewGuid();
            Guid bookId2 = Guid.NewGuid();
            var books = new List<Book>
            {
                new Book { Id = bookId1, Title = "Book 1" },
                new Book { Id = bookId2, Title = "Book 2" }
            };
            _mockRepository.Setup(r => r.GetAllBooks()).Returns(books);
            _mockMapper.Setup(m => m.Map<BookDto>(It.IsAny<Book>()))
                       .Returns<Book>(b => new BookDto { Id = b.Id, Title = b.Title });

            var result = _bookService.GetAllBooks();

            Assert.Equal(2, result.Count);
            Assert.Equal("Book 1", result[0].Title);
            Assert.Equal(bookId1, result[0].Id);
            Assert.Equal("Book 2", result[1].Title);
            Assert.Equal(bookId2, result[1].Id);
        }

        [Fact]
        public void GetAllBooks_ReturnsEmptyList_WhenNoBooks()
        {
            _mockRepository.Setup(r => r.GetAllBooks()).Returns(new List<Book>());

            var result = _bookService.GetAllBooks();

            Assert.Empty(result);
        }

        [Fact]
        public void GetBook_ReturnsCorrectBook_WhenBookExists()
        {
            var bookId = Guid.NewGuid();
            var book = new Book { Id = bookId, Title = "Test Book" };
            _mockRepository.Setup(r => r.GetBook(bookId)).Returns(book);
            _mockMapper.Setup(m => m.Map<BookDto>(book))
                       .Returns(new BookDto { Id = book.Id, Title = book.Title });

            var result = _bookService.GetBook(bookId);

            Assert.Equal(bookId, result.Id);
            Assert.Equal("Test Book", result.Title);
            Assert.Equal(bookId, result.Id);
        }

        [Fact]
        public void GetBook_ThrowsException_WhenBookDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetBook(nonExistentId)).Returns((Book?)null);

            Assert.Throws<ArgumentException>(() => _bookService.GetBook(nonExistentId));
        }

        [Fact]
        public void ValidateCreateDto_ThrowsException_WhenTitleTooLong()
        {
            var bookDto = new BookDto { Title = new string('A',51), AuthorId = Guid.NewGuid() };

            var exception = Assert.Throws<ArgumentException>(() => _bookService.ValidateCreateDto(bookDto));
        }

        [Fact]
        public void ValidateCreateDto_ThrowsException_WhenAuthorIdEmpty()
        {
            var bookDto = new BookDto { Title = "Valid Title", AuthorId = Guid.Empty };

            var exception = Assert.Throws<ArgumentException>(() => _bookService.ValidateCreateDto(bookDto));
        }

        [Fact]
        public void ValidateCreateDto_ThrowsException_WhenAuthorDoesNotExist()
        {
            var bookDto = new BookDto { Title = "Valid Title", AuthorId = Guid.NewGuid() };
            _mockRepository.Setup(r => r.AuthorExists(bookDto.AuthorId)).Returns(false);

            var exception = Assert.Throws<ArgumentException>(() => _bookService.ValidateCreateDto(bookDto));
        }

        [Fact]
        public void ValidateCreateDto_ThrowsException_WhenBookTitleExists()
        {
            var bookDto = new BookDto { Title = "Existing Title", AuthorId = Guid.NewGuid() };
            _mockRepository.Setup(r => r.AuthorExists(bookDto.AuthorId)).Returns(true);
            _mockRepository.Setup(r => r.BookTitleExists(bookDto.Title)).Returns(true);

            var exception = Assert.Throws<ArgumentException>(() => _bookService.ValidateCreateDto(bookDto));
        }

        [Fact]
        public void ValidateCreateDto_DoesNotThrow_WhenAllValidationsPassed()
        {
            var bookDto = new BookDto { Title = "Valid Title", AuthorId = Guid.NewGuid() };
            _mockRepository.Setup(r => r.AuthorExists(bookDto.AuthorId)).Returns(true);
            _mockRepository.Setup(r => r.BookTitleExists(bookDto.Title)).Returns(false);

            var exception = Record.Exception(() => _bookService.ValidateCreateDto(bookDto));
            Assert.Null(exception);
        }
}