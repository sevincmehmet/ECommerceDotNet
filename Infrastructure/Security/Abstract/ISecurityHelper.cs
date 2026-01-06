namespace Infrastructure.Security.Abstract
{
    public interface ISecurityHelper
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashString);


    }
}
