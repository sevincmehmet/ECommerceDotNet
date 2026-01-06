using DataAccess.Abstract.Sales;
using Entities.Concrete.Sales;

namespace DataAccess.Concrete.EntityFramework.Sales
{
    public class EfPaymentTransictionRepository : EfRepositoryBase<PaymentTransaction>, IPaymentTransactionRepository
    {
    }
}
