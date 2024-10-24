using AutoMapper;
using Google.Apis.Util;
using Microsoft.AspNetCore.Mvc;
using WonderingBookApi.DTOs.Article;
using WonderingBookApi.Models;
using WonderingBookApi.Services;
using WonderingBookApi.Services.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WonderingBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IBookService _bookService;
        private readonly IHandleFirebaseService _storage;
        private readonly IMapper _mapper;
        public ArticlesController(
            IArticleService articleService, 
            IMapper mapper, 
            IHandleFirebaseService storage, 
            IBookService bookService) 
        {
            _articleService = articleService;
            _mapper = mapper;
            _storage = storage;
            _bookService = bookService;
        }

        // GET: api/<ArticlesController>
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return Ok(articles);
        }

        // GET api/<ArticlesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(Guid id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            if (article == null)
                return NotFound();

            return Ok(article);
        }
        // GET api/<ArticlesController>/search
        [HttpGet("search-by-book")]
        public async Task<IActionResult> GetArticleByBook(string name)
        {
            var article = await _articleService.GetArticlesByBookNameAsync(name);
            if (article == null)
                return NotFound();
            return Ok(article);

        }
        [HttpGet("recommend")]
        public async Task<IActionResult> RecommendArticles()
        {
            var article = await _articleService.RecommendArticles();
            return Ok(article);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetArticleByUserId(Guid userId)
        {
            var article = await _articleService.GetArticlesByUserIdAsync(userId);
            if (article == null)
                return NotFound();

            return Ok(article);
        }

        // POST api/<ArticlesController>
        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDTO newArticle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            var article = _mapper.Map<Article>(newArticle);
            if (newArticle.Book != null)
            {
                if (await _bookService.CheckExistAsync(newArticle.Book.Id))
                {
                    article.BookId = newArticle.Book.Id;
                }
                else
                {
                    var createdBook = await _bookService.CreateBookAsync(_mapper.Map<Book>(newArticle.Book));
                }
                
            }
            article.ArticleId = Guid.NewGuid();
            if(newArticle.DefaultImage != null)
                article.Image = newArticle.DefaultImage;
            if (newArticle.Image != null)
                article.Image = await _storage.UploadImageAsync(newArticle.Image, article.ArticleId);
            var createdArticle = await _articleService.CreateArticleAsync(article);
            return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.ArticleId }, createdArticle);
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(Guid id, [FromForm] UpdateArticleDTO updateArticle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var article = await _articleService.GetArticleByIdAsync(id);
                if (article == null)
                    return NotFound();

                _mapper.Map(updateArticle, article);
                if (updateArticle.DefaultImage != null)
                    article.Image = updateArticle.DefaultImage;
                if (updateArticle.Image != null)
                    article.Image = await _storage.UploadImageAsync(updateArticle.Image, article.ArticleId);
                await _articleService.UpdateArticleAsync(article);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _articleService.DeleteArticleAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
