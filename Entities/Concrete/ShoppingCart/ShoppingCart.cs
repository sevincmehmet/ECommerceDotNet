using Core.Entities.Abstract;
using Entities.Concrete.Users;

namespace Entities.Concrete.ShoppingCart
{
    public class ShoppingCart : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Fk
        public DateTime CreatedDate { get; set; }

        // Navigation Property
        public virtual User User { get; set; }
    }
}
