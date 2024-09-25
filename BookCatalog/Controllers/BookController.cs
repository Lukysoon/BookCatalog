using BookCatalog.Models.Dtos;
using BookCatalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateBook([FromBody] BookDto book)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest($"Model state is not valid");
                
                _bookService.ValidateCreateDto(book);

                _bookService.CreateBook(book);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (ArgumentException ex){
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBook([FromRoute] Guid id)
        {
            try
            {
                if (!_bookService.Exists(id))
                    return NotFound();

                var book = _bookService.GetBook(id);

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateBook([FromBody] BookDto book)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Model state is not valid");

                if (!_bookService.Exists(book.Id))
                    return NotFound();

                _bookService.ValidateUpdateDto(book);

                _bookService.Update(book);
                return Ok();
            }
            catch (ArgumentException ex){
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteBook([FromRoute] Guid id)
        {
            try
            {
                if (!_bookService.Exists(id))
                    return NotFound();

                _bookService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllBooks()
        {
            try
            {
                List<BookDto> books = _bookService.GetAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}