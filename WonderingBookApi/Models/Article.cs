using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WonderingBookApi.Utilities;

namespace WonderingBookApi.Models
{
    public class Article
    {
        [Key]
        public Guid ArticleId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [ForeignKey("Book")]
        public string? BookId { get; set; }
        public string? MiscAuthor { get; set; }
        public string CuratorNote { get; set; } = string.Empty;
        public string? Image { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public ArticleStatus? Status { get; set; } = ArticleStatus.Draft; 

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
        public virtual ICollection<ArticleTopic> ArticleTopics { get; set; }
        public virtual ICollection<IdeaCard> IdeaCards { get; set; }
    }
}
