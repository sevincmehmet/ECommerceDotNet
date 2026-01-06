using DataAccess.Abstract.Users;
using Entities.Concrete.Users;

namespace DataAccess.Concrete.EntityFramework.Users
{
    public class EfUserPermissionRepository : EfRepositoryBase<UserPermission>, IUserPermissionRepository
    {
    }
}

