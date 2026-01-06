using Core.Entities.Abstract;
using Entities.Concrete.Orders;

namespace Entities.Concrete.Sales
{
    public class PaymentTransaction : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Fk
        public string TransactionCode { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime TransactionDate { get; set; }

        // Navigation Property
        public virtual Order order { get; set; }
    }
}
