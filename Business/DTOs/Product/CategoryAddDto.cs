namespace Business.DTOs.Product
{

    public class CategoriyAddDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; } // en üstekinin parentı olmayabilir.
    }
}

