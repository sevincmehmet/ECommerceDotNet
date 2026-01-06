using Core.Entities.Abstract;
using Entities.Concrete.Products;

namespace Entities.Concrete.ShoppingCart
{
    public class CartItem : IEntity
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; } // Fk
        public int ProductId { get; set; } // Fk
        public short Quantity { get; set; }

        // Navigation Properties
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual Product Product { get; set; }
    }
}
