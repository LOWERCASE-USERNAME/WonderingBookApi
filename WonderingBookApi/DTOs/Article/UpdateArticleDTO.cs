namespace WonderingBookApi.DTOs.Article
{
    public class UpdateArticleDTO
    {
        public string Title { get; set; }
        public string CuratorNote { get; set; } = string.Empty;
        public string? MiscAuthor { get; set; }
        public IFormFile? Image { get; set; }
        public string? DefaultImage { get; set; }
    }
}
