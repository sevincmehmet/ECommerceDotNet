using Business.DTOs.Product;
using Entities.Concrete.Products;
using Infrastructure.Result.Abstract;

namespace Business.Abstract.Products
{
    public interface ICategoryService
    {
        IDataResult<List<CategoriyDto>> GetAll();
        IDataResult<CategoriyDto> GetById(int id);
        IDataResult<List<Product>> GetCategoryProductBySlug(string slug);
        IDataResult<ProductCategoryDto> GetCategoryProductById(int id);
        IDataResult<CategoriyDto> AddCategory(CategoriyAddDto category);
        IDataResult<CategoriyDto> UpdateCategory(int id, CategoriyUpdateDto category);
        IResult DeleteCategory(int id);
    }
}
