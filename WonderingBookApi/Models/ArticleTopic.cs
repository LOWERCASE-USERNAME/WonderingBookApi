using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.Models
{
    public class ArticleTopic
    {
        [Key]
        public Guid ArticleTopicId { get; set; }
        [Required]
        public Guid ArticleId { get; set; }
        [Required]
        public int TopicId { get; set; }

        // Navigation properties
        public virtual Article Article { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
