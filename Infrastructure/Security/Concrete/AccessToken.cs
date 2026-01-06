namespace Infrastructure.Security.Concrete
{
    public class AccessToken(string token, DateTime expiration)
    {
        public string Token { get; set; } = token;
        public DateTime Expiration { get; set; } = expiration;
    }
}
