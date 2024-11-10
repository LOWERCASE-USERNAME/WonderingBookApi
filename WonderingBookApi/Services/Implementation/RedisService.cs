using StackExchange.Redis;

namespace WonderingBookApi.Services.Implementation
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisService(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
        }

        // Set a view limit, e.g., 5 views per day
        private const int ViewLimit = 10;

        public async Task<int> GetArticleViews(string userId)
        {
            var viewCount = await _db.StringGetAsync($"ArticleViews:{userId}");
            return viewCount.HasValue ? (int)viewCount : 0;
        }

        public async Task<bool> IncrementArticleViews(string userId)
        {
            var key = $"ArticleViews:{userId}";

            DateTime now = DateTime.Now;
            DateTime midnight = now.Date.AddDays(1);
            TimeSpan timeUntilMidnight = midnight - now;
            

            if (!await _db.KeyExistsAsync(key))
            {
                // Set the key with an expiration until midnight if it doesn't exist
                await _db.StringIncrementAsync(key);
                await _db.KeyExpireAsync(key, timeUntilMidnight);
            }
            else
            {
                // Increment the existing key's view count
                await _db.StringIncrementAsync(key);
            }
            // Return the current view count
            var viewCount = await GetArticleViews(userId);

            return viewCount <= ViewLimit;
        }

        public async Task ResetArticleViews(string userId)
        {
            await _db.KeyDeleteAsync($"ArticleViews:{userId}");
        }
    }
}
