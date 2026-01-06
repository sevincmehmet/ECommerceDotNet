using Core.Entities.Abstract;

namespace Entities.Concrete.Users
{
    public class UserRole : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

    }
}
