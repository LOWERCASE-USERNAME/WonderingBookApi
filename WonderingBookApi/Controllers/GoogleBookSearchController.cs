using WonderingBookApi.Models;
using WonderingBookApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Books.v1.Data;

namespace WonderingBookApi.Controllers
{
    //TODO: OPTIMIZE PERFORMANCE (GOOGLE BOOKS API DOCUMENT)

    [Route("api/[controller]")]
    [ApiController]
    public class GoogleBookSearchController : ControllerBase
    {
        private readonly GoogleBookSearchService _bookService;

        public GoogleBookSearchController(GoogleBookSearchService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GoogleBook>> GetBook(string id)
        {
            var book = await _bookService.GetBookAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<GoogleBook>>> SearchBooks([FromQuery] string query, [FromQuery] int maxResults = 10, [FromQuery]int startIndex = 0, [FromQuery]string langRestrict = "en")
        {
            var books = await _bookService.SearchBooksAsync(query, maxResults, startIndex, langRestrict);
            return Ok(books);
        }

        [HttpGet("searchByTitle")]
        public async Task<ActionResult<IEnumerable<GoogleBook>>> SearchBooksByTitle([FromQuery] string title, [FromQuery] int maxResults = 10, [FromQuery] int startIndex = 0, [FromQuery] string langRestrict = "en")
        {
            var books = await _bookService.SearchBooksByTitleAsync(title, maxResults, startIndex, langRestrict);
            return Ok(books);
        }

        [HttpGet("searchByAuthor")]
        public async Task<ActionResult<IEnumerable<GoogleBook>>> SearchBooksByAuthor([FromQuery] string author, [FromQuery] int maxResults = 10, [FromQuery] int startIndex = 0, [FromQuery] string langRestrict = "en")
        {
            var books = await _bookService.SearchBooksByAuthorAsync(author, maxResults, startIndex, langRestrict);
            return Ok(books);
        }

        [HttpGet("searchByISBN")]
        public async Task<ActionResult<IEnumerable<GoogleBook>>> SearchBooksByISBN([FromQuery] string isbn, [FromQuery] int maxResults = 10, [FromQuery] int startIndex = 0, [FromQuery] string langRestrict = "en")
        {
            var books = await _bookService.SearchBooksByISBNAsync(isbn, maxResults, startIndex, langRestrict);
            return Ok(books);
        }
    }
}
