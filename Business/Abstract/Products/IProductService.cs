using Business.DTOs.Product;
using Infrastructure.Result.Abstract;

namespace Business.Abstract.Products
{
    public interface IProductService
    {
        IDataResult<List<ProductDto>> GetAll();
        IDataResult<ProductDto> GetById(int productId);
        IDataResult<ProductUpdateDto> UpdateProduct(int productId, ProductUpdateDto dto);
        IResult DeleteProduct(int productId);
        IDataResult<ProductAddDto> CreateProduct(ProductAddDto dto);
        IDataResult<List<ProductDto>> GetProductsByCategoryId(int categoryId);
        IDataResult<List<ProductDto>> GetProductsByCategorySlug(string categorySlug);
        IDataResult<ProductMeAddDto> CreateMyProduct(ProductMeAddDto dto);
        IDataResult<ProductUpdateDto> UpdateMyProduct(ProductUpdateDto dto);
        IResult DeleteMyProduct(int productId);

    }
}
