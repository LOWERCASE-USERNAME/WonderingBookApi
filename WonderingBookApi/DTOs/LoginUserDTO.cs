namespace WonderingBookApi.DTOs
{
    public class LoginUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}
