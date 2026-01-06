using Core.Entities.Abstract;
using Entities.Concrete.Users;

namespace Entities.Concrete.Products
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } // Fk
        public int CreatedByUserId { get; set; } // Fk
        public int BrandId { get; set; } // Fk
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation Properties
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual User CreatedByUser { get; set; }
    }
}
