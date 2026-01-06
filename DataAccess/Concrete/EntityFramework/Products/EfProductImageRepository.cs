using DataAccess.Abstract.Products;
using Entities.Concrete.Products;

namespace DataAccess.Concrete.EntityFramework.Products
{
    public class EfProductImageRepository : EfRepositoryBase<ProductImage>, IProductImageRepository
    {
    }

}
