using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public string AuthorNotes { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
        public virtual ICollection<ArticleTopic> ArticleTopics { get; set; }
        public virtual ICollection<IdeaCard> IdeaCards { get; set; }
    }
}
