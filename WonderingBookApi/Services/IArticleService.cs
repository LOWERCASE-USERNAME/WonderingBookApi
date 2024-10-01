using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IArticleService
    {
        Task<Article> CreateArticleAsync(Article newArticle);
        Task<Article> GetArticleByIdAsync(Guid id);
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task UpdateArticleAsync(Article updatedArticle);
        // Task DeleteArticleAsync(Guid id);
    }
}
