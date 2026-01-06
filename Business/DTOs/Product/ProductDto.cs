using Entities.Concrete.Products;

namespace Business.DTOs.Product
{
    public class ProductDto
    {
        // Categori yolu eklenebilir...

        public int Id { get; set; }
        
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Category Category { get; set; }

    }
}
