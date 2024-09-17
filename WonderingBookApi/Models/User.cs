using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
