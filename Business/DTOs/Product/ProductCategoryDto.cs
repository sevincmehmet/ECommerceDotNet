namespace Business.DTOs.Product
{
    public class ProductCategoryDto
    {
        public List<string> BreadCrumb { get; set; }
        public string Slug { get; set; }
        public List<CategoriyDto> Categoriys { get; set; }
    }
}
