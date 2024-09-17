using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.Models
{
    public class SavedIdea
    {
        [Key]
        public int SavedIdeaId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ArticleId { get; set; }
        // Navigation properties
        public virtual User User { get; set; }
        public virtual Article Article { get; set; }
    }
}
