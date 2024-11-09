using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WonderingBookApi.Utilities;

namespace WonderingBookApi.Models
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime")]
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime LastActiveAt { get; set; } = DateTime.Now;
        public bool IsAdmin { get; set; } = false;
        
        [Required]
        public UserStatus? Status { get; set; } = UserStatus.Active;
        // Navigation properties
        [JsonIgnore]
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<SavedIdea> SavedIdeas { get; set; }
        public virtual Wallet? Wallet { get; set; }
    }
}
