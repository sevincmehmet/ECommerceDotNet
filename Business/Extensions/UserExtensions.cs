using DataAccess.Abstract.Users;

namespace Business.Extensions
{
    public class UserExtension
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserPermissionRepository _userPermissionRepository;

        public UserExtension(IUserRoleRepository userRoleRepository, IUserPermissionRepository userPermissionRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userPermissionRepository = userPermissionRepository;
        }

        public List<string> GetUserTotalPermissions(int userId)
        {
            // 1. Rollerden gelen yetkiler (Burada Include kullanarak Role ve RolePermissions'ı çekmeyi unutma yoksa null döner)
            var rolePermissions = _userRoleRepository.GetList(ur => ur.UserId == userId)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.Name);

            // 2. Kullanıcıya özel tanımlanmış yetkiler (IsDenied olmayanlar)
            var directPermissions = _userPermissionRepository.GetList(up => up.UserId == userId && !up.IsDenied)
                .Select(up => up.Permission.Name);

            // 3. Engellenmiş (IsDenied) yetkiler
            var deniedPermissions = _userPermissionRepository.GetList(up => up.UserId == userId && up.IsDenied)
                .Select(up => up.Permission.Name);

            return rolePermissions.Union(directPermissions).Except(deniedPermissions).Distinct().ToList();
        }
    }
}