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
        public int IdeaCardId { get; set; }
        // Navigation properties
        public virtual User User { get; set; }
        public virtual IdeaCard IdeaCard { get; set; }
    }
}
