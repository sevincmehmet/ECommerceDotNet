using DataAccess.Abstract.Orders;
using Entities.Concrete.Orders;

namespace DataAccess.Concrete.EntityFramework.Orders
{
    public class EfShipmentRepository : EfRepositoryBase<Shipment>, IShipmentRepository
    {
    }
}
