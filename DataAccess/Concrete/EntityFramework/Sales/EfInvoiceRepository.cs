using DataAccess.Abstract.Sales;
using Entities.Concrete.Sales;

namespace DataAccess.Concrete.EntityFramework.Sales
{
    public class EfInvoiceRepository : EfRepositoryBase<Invoice>, IInvoiceRepository
    {
    }
}
