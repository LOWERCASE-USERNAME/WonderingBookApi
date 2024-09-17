using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.Models
{
    public class IdeaCard
    {
        [Key]
        public int IdeaCardId { get; set; }
        [Required]
        public int ArticleId { get; set; }
        public int CardType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }

        // Navigation properties
        public virtual Article Article { get; set; }
        public virtual ICollection<SavedIdea> SavedIdeas { get; set;}
    }
}
