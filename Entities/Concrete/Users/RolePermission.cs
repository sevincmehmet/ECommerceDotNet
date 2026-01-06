using Core.Entities.Abstract;

namespace Entities.Concrete.Users
{
    public class RolePermission : IEntity
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }

        // Navigation
        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
