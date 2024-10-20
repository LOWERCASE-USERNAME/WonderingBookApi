using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IArticleService
    {
        Task<Article> CreateArticleAsync(Article newArticle);
        Task<Article> GetArticleByIdAsync(Guid id);
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task UpdateArticleAsync(Article updatedArticle);
        Task<IEnumerable<Article>> GetArticlesByUserIdAsync(Guid userId);
        Task<IEnumerable<Article>> GetArticlesByBookNameAsync(string name);
        Task<IEnumerable<Article>> RecommendArticles();
        // Task DeleteArticleAsync(Guid id);
    }
}
