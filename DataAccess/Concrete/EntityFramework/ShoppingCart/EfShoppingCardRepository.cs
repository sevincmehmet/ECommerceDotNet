using DataAccess.Abstract.ShoppingCart;

namespace DataAccess.Concrete.EntityFramework.ShoppingCart
{
    public class EfShoppingCardRepository : EfRepositoryBase<Entities.Concrete.ShoppingCart.ShoppingCart>, IShoppingCartRepository
    {
    }
}
