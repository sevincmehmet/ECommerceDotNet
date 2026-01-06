using Core.Entities.Abstract;

namespace Entities.Concrete.Users
{
    public class Permission : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
