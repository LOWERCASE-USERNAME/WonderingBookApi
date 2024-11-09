using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WonderingBookApi.Data;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IIdeaCardService _ideaCardService;
        private readonly IHandleFirebaseService _handleFirebaseService;

        public ArticleService(ApplicationDbContext context, IIdeaCardService ideaCardService, IHandleFirebaseService handleFirebaseService)
        {
            _context = context;
            _ideaCardService = ideaCardService;
            _handleFirebaseService = handleFirebaseService;
        }

        public async Task<Article> CreateArticleAsync(Article newArticle)
        {
            newArticle.DateCreated = DateTime.UtcNow;
            newArticle.Book = null;
            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();
            return newArticle;
        }

        public async Task<Article> GetArticleByIdAsync(Guid id)
        {
            var article = await _context.Articles.Include(article => article.IdeaCards).FirstOrDefaultAsync(a => a.ArticleId == id);
            return article;
        }

        public async Task<List<Article>> GetListArticleByIdAsync(List<Guid> listIds)
        {
            var articles = await _context.Articles.Where(a => listIds.Contains(a.ArticleId)).ToListAsync();
            return articles;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetAllArticlesExtendedAsync()
        {
            return await _context.Articles
                .Include(a => a.Book)
                .Include(a => a.IdeaCards)
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task UpdateArticleBulkAsync(List<Article> updatedArticles)
        {
            if (updatedArticles == null || !updatedArticles.Any())
                throw new ArgumentNullException(nameof(updatedArticles));
            try
            {
                _context.Articles.UpdateRange(updatedArticles);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
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

        public async Task<IEnumerable<Article>> GetArticlesByBookIdAsync(string id)
        {
            return await _context.Articles
                 .Include(a => a.Book)
                 .Where(a => a.Book.Id.ToLower().Equals(id.ToLower()))
                 .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticlesByBookNameAsync(string name)
        {
           return await _context.Articles
                .Include(a => a.Book)
                .Where(a => a.Book.Title != null && a.Book.Title.ToLower()
                .Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> RecommendArticles()
        {
            //Take random 5 articles
            var article = await _context.Articles.OrderBy(x => Guid.NewGuid()).Take(5).ToListAsync();
            return article;

        }

        public async Task DeleteArticleAsync(Guid articleId)
        {
            try
            {
                Article article = await _context.Articles.FirstOrDefaultAsync(a => a.ArticleId == articleId);
                if(article == null) throw new Exception($"Article with ID {articleId} not found.");

                // Get list of ideaCards
                List<IdeaCard> ideaCards = await _context.IdeaCards
                    .Where(ic => ic.ArticleId == articleId)
                    .ToListAsync();

                await _ideaCardService.DeleteIdeaCardsAsync(ideaCards.Select(ic => ic.IdeaCardId).ToList());

                // Remove image from firebase
                if(!string.IsNullOrEmpty(article.Image))
                {
                    await _handleFirebaseService
                    .DeleteImageAsync(new List<string>() { article.Image });
                }

                // Remove the article from the context
                _context.Articles.Remove(article);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
