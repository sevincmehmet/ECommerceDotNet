using DataAccess.Abstract.Sales;
using Entities.Concrete.Sales;

namespace DataAccess.Concrete.EntityFramework.Sales
{
    public class EfCouponRepository : EfRepositoryBase<Coupon>, ICouponRepository
    {
    }
}
