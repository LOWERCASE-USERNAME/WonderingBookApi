using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WonderingBookApi.Models;
using WonderingBookApi.Services;

namespace WonderingBookApi.Controllers
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

        [HttpGet("search-by-name")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooksByName([FromQuery] string query)
        {
            IEnumerable<Book> books = await _bookService.GetBooksByNameAsync(query);
            return Ok(books);
        }
    }
}
