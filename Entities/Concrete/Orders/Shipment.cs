using Core.Entities.Abstract;

namespace Entities.Concrete.Orders
{
    public class Shipment : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Fk
        public string TrackingNumber { get; set; }
        public string ShippingCompany { get; set; }
        public string ShippingStatus { get; set; }
        public DateTime ShippingDate { get; set; }

        // Navigation Property
        public virtual Order Order { get; set; }
    }
}
