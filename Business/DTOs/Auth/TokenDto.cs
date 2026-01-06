namespace Business.DTOs.Auth
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
