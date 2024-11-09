using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IArticleService
    {
        Task<Article> CreateArticleAsync(Article newArticle);
        Task<Article> GetArticleByIdAsync(Guid id);
        Task<List<Article>> GetListArticleByIdAsync(List<Guid> listIds);
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task<IEnumerable<Article>> GetAllArticlesExtendedAsync();
        Task UpdateArticleBulkAsync(List<Article> updatedArticles);
        Task UpdateArticleAsync(Article updatedArticle);
        Task<IEnumerable<Article>> GetArticlesByUserIdAsync(Guid userId);
        Task<IEnumerable<Article>> GetArticlesByBookIdAsync(string id);
        Task<IEnumerable<Article>> GetArticlesByBookNameAsync(string name);
        Task<IEnumerable<Article>> RecommendArticles();
        Task DeleteArticleAsync(Guid id);
    }
}
