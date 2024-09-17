using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.Models
{
    public class ArticleTopic
    {
        [Key]
        public int ArticleTopicId { get; set; }
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public int TopicId { get; set; }

        // Navigation properties
        public virtual Article Article { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
