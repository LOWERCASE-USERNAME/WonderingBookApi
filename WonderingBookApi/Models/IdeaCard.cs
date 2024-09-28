using System.ComponentModel.DataAnnotations;
using WonderingBookApi.Utilities;

namespace WonderingBookApi.Models
{
    public class IdeaCard
    {
        [Key]
        public Guid IdeaCardId { get; set; }
        [Required]
        public Guid ArticleId { get; set; }
        public IdeaCardType CardType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public int? Order {  get; set; }

        // Navigation properties
        public virtual Article Article { get; set; }
        public virtual ICollection<SavedIdea> SavedIdeas { get; set;}
    }
}
