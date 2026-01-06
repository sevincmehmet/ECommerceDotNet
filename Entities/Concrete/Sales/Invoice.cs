using Core.Entities.Abstract;
using Entities.Concrete.Orders;

namespace Entities.Concrete.Sales
{
    public class Invoice : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Fk
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation Property
        public virtual Order Order { get; set; }

    }
}
