using DataAccess.Abstract.ShoppingCart;
using Entities.Concrete.ShoppingCart;

namespace DataAccess.Concrete.EntityFramework.ShoppingCart
{
    public class EfCartItemRepository : EfRepositoryBase<CartItem>, ICartItemRepository
    {
    }

}
