using AutoMapper;
using Google.Apis.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using WonderingBookApi.DTOs.Article;
using WonderingBookApi.DTOs.Book;
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

        [HttpGet("admin")]
        public async Task<IActionResult> GetAllArticlesExtended()
        {
            var articles = await _articleService.GetAllArticlesExtendedAsync();

            return Ok(articles.Where(a => a.Status != Utilities.ArticleStatus.Draft));
            //return Ok(articles);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetPublishedArticles()
        {
            var articles = await _articleService.GetAllArticlesExtendedAsync();

            return Ok(articles.Where(a => a.Status == Utilities.ArticleStatus.Published));
            //return Ok(articles);
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

        [HttpGet("search-by-book-id")]
        public async Task<IActionResult> GetArticleByBookId(string id)
        {
            var article = await _articleService.GetArticlesByBookIdAsync(id);
            if (article == null)
                return NotFound();
            return Ok(article);
        }

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
        public async Task<IActionResult> CreateArticle([FromForm] CreateArticleDTO newArticle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            Article article = _mapper.Map<Article>(newArticle);
            if (!string.IsNullOrEmpty(newArticle.SerializedBook))
            {
                CreateBookDTO bookDto = null;
                try
                {
                    bookDto = JsonConvert.DeserializeObject<CreateBookDTO>(newArticle.SerializedBook);
                }
                catch (JsonException)
                {
                    return BadRequest("Invalid book data format.");
                }

                if (await _bookService.CheckExistAsync(bookDto.Id))
                {
                    article.BookId = bookDto.Id;
                }
                else
                {
                    var createdBook = await _bookService.CreateBookAsync(_mapper.Map<Book>(bookDto));
                    article.BookId = createdBook.Id;
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

        [HttpPut("update-status-bulk")]
        public async Task<IActionResult> UpdateArticleStatusBulk([FromBody] List<UpdateArticleStatusDTO> updateStatusArticles)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var articles = await _articleService.GetListArticleByIdAsync(updateStatusArticles.Select(dto => dto.ArticleId).ToList());
                if (articles == null || !articles.Any())
                    return NotFound();

                foreach (var article in articles)
                {
                    var statusUpdate = updateStatusArticles.FirstOrDefault(dto => dto.ArticleId == article.ArticleId);
                    if (statusUpdate != null)
                    {
                        article.Status = statusUpdate.Status;
                    }
                }
                await _articleService.UpdateArticleBulkAsync(articles);
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

        // PUT: api/user/publish/{articleId}
        [HttpPut("publish/{id}")]
        public async Task<IActionResult> PublishArticle(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var article = await _articleService.GetArticleByIdAsync(id);
                if (article == null)
                    return NotFound();
                article.Status = User.IsInRole("ContentProvider") ? Utilities.ArticleStatus.Published : Utilities.ArticleStatus.Pending;
                await _articleService.UpdateArticleAsync(article);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        // PUT: api/user/approve/{articleId}
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveArticle(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var article = await _articleService.GetArticleByIdAsync(id);
                if (article == null)
                    return NotFound();
                article.Status = Utilities.ArticleStatus.Published;
                await _articleService.UpdateArticleAsync(article);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        // PUT: api/user/notapprove/{articleId}
        [HttpPut("notapprove/{id}")]
        public async Task<IActionResult> NotApproveArticle(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var article = await _articleService.GetArticleByIdAsync(id);
                if (article == null)
                    return NotFound();
                article.Status = Utilities.ArticleStatus.NotApproved;
                await _articleService.UpdateArticleAsync(article);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }



    }
}
