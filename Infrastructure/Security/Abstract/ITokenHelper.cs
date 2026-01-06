using Entities.Concrete.Users;
using AccessToken = Infrastructure.Security.Concrete.AccessToken;

namespace Infrastructure.Security.Abstract
{
    public interface ITokenHelper
    {
        // Artık sadece roller (userClaims) değil, yetkiler (permissions) de geliyor
        AccessToken CreateToken(User user, List<string> roles, List<string> permissions);
    }
}