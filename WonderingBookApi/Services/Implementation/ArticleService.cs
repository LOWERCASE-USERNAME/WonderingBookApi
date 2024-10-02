using Microsoft.EntityFrameworkCore;
using WonderingBookApi.Data;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Article> CreateArticleAsync(Article newArticle)
        {
            newArticle.ArticleId = Guid.NewGuid();
            newArticle.DateCreated = DateTime.UtcNow;
            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();
            return newArticle;
        }

        public async Task<Article> GetArticleByIdAsync(Guid id)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.ArticleId == id);
            return article;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task UpdateArticleAsync(Article updatedArticle)
        {
            if (updatedArticle == null)
                throw new ArgumentNullException(nameof(updatedArticle));
            try
            {
                _context.Entry(updatedArticle).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Article>> GetArticlesByUserIdAsync(Guid userId)
        {
            var article = await _context.Articles.Where(a => a.UserId == userId.ToString()).ToListAsync();
            return article;
        }
    }
}
