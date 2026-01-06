using Core.Entities.Abstract;

namespace Entities.Concrete.Users
{
    public class UserPermission : IEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }

        public bool IsDenied { get; set; } // Bazen bir yetkiyi kullanıcı elinden özellikle almak için eklendi.
    }
}
    