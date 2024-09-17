using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }
        [Required]
        [StringLength(40)]
        public string TopicName { get; set; }

        // Navigation property
        public virtual ICollection<ArticleTopic> ArticleTopics { get; set; }
    }
}
