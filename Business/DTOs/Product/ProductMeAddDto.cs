namespace Business.DTOs.Product
{
    public class ProductMeAddDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } // Fk
        public int BrandId { get; set; } // Fk
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}
