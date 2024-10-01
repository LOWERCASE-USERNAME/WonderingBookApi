using AutoMapper;
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
        private readonly IMapper _mapper;
        public ArticlesController(IArticleService articleService, IMapper mapper) 
        {
            _articleService = articleService;
            _mapper = mapper;
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

        // POST api/<ArticlesController>
        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDTO newArticle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var article = _mapper.Map<Article>(newArticle);
            var createdArticle = await _articleService.CreateArticleAsync(article);
            return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.ArticleId }, createdArticle);
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(Guid id, [FromBody] UpdateArticleDTO updateArticle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var article = await _articleService.GetArticleByIdAsync(id);
                if (article == null)
                    return NotFound();

                _mapper.Map(updateArticle, article);
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
        public void Delete(int id)
        {
        }
    }
}
