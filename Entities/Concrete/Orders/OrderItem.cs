using Core.Entities.Abstract;
using Entities.Concrete.Products;

namespace Entities.Concrete.Orders
{
    public class OrderItem : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Fk
        public int ProductId { get; set; } // Fk
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation Properties
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

    }
}
