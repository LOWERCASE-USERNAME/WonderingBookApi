using System.ComponentModel.DataAnnotations;
using WonderingBookApi.Utilities;

namespace WonderingBookApi.DTOs.IdeaCard
{
    public class UpdateIdeaCardDTO
    {
        public Guid IdeaCardId { get; set; }
        public Guid ArticleId { get; set; }
        public IdeaCardType CardType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public int? Order { get; set; }
    }
}
