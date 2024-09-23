using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IArticleService
    {
        Task<Article> CreateArticleAsync(Article newArticle);
        Task<Article> GetArticleByIdAsync(int id);
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task UpdateArticleAsync(int id, Article updatedArticle);
        // Task DeleteArticleAsync(Guid id);
    }
}
