using Business.DTOs.Product;
using Infrastructure.Result.Abstract;

namespace Business.Abstract.Products
{
    public interface IBrandService
    {
        IDataResult<BrandDto> AddBrand(BrandDto brand);
        IResult DeleteBrand(int brandId);
        IDataResult<BrandDto> UpdateBrand(int brandId, BrandDto brand);
        IDataResult<BrandDto> GetBrand(int brandId);
        IDataResult<List<BrandDto>> GetAll();
    }
}
