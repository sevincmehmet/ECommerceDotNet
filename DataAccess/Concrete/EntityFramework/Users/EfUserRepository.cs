using DataAccess.Abstract.Users;
using Entities.Concrete.Users;

namespace DataAccess.Concrete.EntityFramework.Users
{
    public class EfUserRepository : EfRepositoryBase<User>, IUserRepository
    {
    }
}
