namespace WonderingBookApi.DTOs.Article
{
    public class CreateArticleDTO
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
    }
}
