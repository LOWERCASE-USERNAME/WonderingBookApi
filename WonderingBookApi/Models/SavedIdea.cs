using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WonderingBookApi.Models
{
    public class SavedIdea
    {
        [Key]
        public Guid SavedIdeaId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public Guid IdeaCardId { get; set; }
        // Navigation properties
        public virtual User User { get; set; }
        public virtual IdeaCard IdeaCard { get; set; }
    }
}
