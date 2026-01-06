using Core.Entities.Abstract;
using Entities.Concrete.Users;

namespace Entities.Concrete.Orders
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Fk
        public DateTime OrderDate { get; set; }
        public bool OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public int ShippingAddressId { get; set; } // Fk

        // Navigation Properties

        public virtual User User { get; set; }
        public virtual Address Address { get; set; }
    }
}
