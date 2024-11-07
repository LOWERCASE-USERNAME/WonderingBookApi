using System.ComponentModel.DataAnnotations;
using WonderingBookApi.Utilities;

namespace WonderingBookApi.DTOs
{
    public class EditUserDTO
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? Fullname { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? UserName { get; set; }

        [Required]
        public UserStatus? Status { get; set; }

        public List<string>? Roles { get; set; }
    }
}
