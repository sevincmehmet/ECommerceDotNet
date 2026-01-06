using Core.Entities.Abstract;

namespace Entities.Concrete.Users
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
