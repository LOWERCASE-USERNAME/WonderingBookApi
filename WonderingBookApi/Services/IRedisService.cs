namespace WonderingBookApi.Services
{
    public interface IRedisService
    {
        Task<int> GetArticleViews(string userId);
        Task<bool> IncrementArticleViews(string userId);
        Task ResetArticleViews(string userId);
    }
}
