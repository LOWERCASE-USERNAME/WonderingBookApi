namespace WonderingBookApi.DTOs.Article
{
    public class CreateArticleDTO
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string? MiscAuthor { get; set; }
        public string CuratorNote { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
    }
}
