using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Username { get; set; }
        public string ProfileImage { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        // Navigation properties
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<SavedIdea> SavedIdeas { get; set; }
    }
}
