using WonderingBookApi.Utilities;

namespace WonderingBookApi.DTOs
{
    public class EditUserDTO
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }

        public UserStatus? Status { get; set; }

        public List<string>? Roles { get; set; }
    }
}
