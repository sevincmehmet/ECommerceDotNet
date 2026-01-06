using Core.Entities.Abstract;

namespace Entities.Concrete.Products
{
    public class ProductImage : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // Fk
        public string ImagePath { get; set; }
        public bool IsMain { get; set; }

        // Navigation Property
        public virtual Product Product { get; set; }
    }
}
