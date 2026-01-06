using Business.Abstract.Products;
using Business.DTOs.Product;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;
using Infrastructure.Result.Abstract;
using Infrastructure.Result.Concrete;

namespace Business.Concrete.Products
{
    public class BrandManagement(IBrandRepostory brandRepostory) : IBrandService
    {
        IBrandRepostory _brandRepostory = brandRepostory ?? throw new ArgumentNullException(nameof(brandRepostory));

        IDataResult<List<BrandDto>> IBrandService.GetAll()
        {
            try
            {
                List<Brand> brandListEntity = _brandRepostory.GetList();
                List<BrandDto> brandListDto = brandListEntity.Select(b => new BrandDto { Id = b.Id, Name = b.Name }).ToList();

                return new SuccessDataResult<List<BrandDto>>(brandListDto, "Liste başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<BrandDto>>("Marka listesi getirilirken bir hata oluştu: " + ex);
            }
        }

        public IDataResult<BrandDto> GetBrand(int brandId)
        {
            var brandEntity = _brandRepostory.Get(b => b.Id == brandId);
            if(brandEntity == null)
            {
                return new ErrorDataResult<BrandDto>($"#{brandId} id değerine sahip bir marka bulunamadı.");
            }
            try
            {
                
                var brandDto = new BrandDto
                {
                    Id = brandEntity.Id,
                    Name = brandEntity.Name,
                };
                return new SuccessDataResult<BrandDto>(brandDto, "Marka başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<BrandDto>("Marka getirilirken bir hata oluştu" + ex);
            }
        }

        public IDataResult<BrandDto> AddBrand(BrandDto dto)
        {
            if (dto.Name.Length < 2)
            {
                return new ErrorDataResult<BrandDto>("Marka ismi 2 harften az olamaz");
            }
            try
            {
                Brand brandEntity = new Brand
                {
                    Id = 0,
                    Name = dto.Name,
                };

                _brandRepostory.Add(brandEntity);
                return new SuccessDataResult<BrandDto>(dto, "Marka başarıyla kaydedildi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<BrandDto>("Marka kaydedilirken bir hata oluştu: " + ex);
            }
        }

        public IDataResult<BrandDto> UpdateBrand(int brandId, BrandDto dto)
        {
            var brandEntity = _brandRepostory.Get(b => b.Id == brandId);
            if (brandEntity == null)
            {
                return new ErrorDataResult<BrandDto>($"#{brandId} id değerine ait bir marka bulunamadı.");
            }

            if (dto.Name.Length < 2)
            {
                return new ErrorDataResult<BrandDto>("Marka ismi 2 harften az olamaz");
            }
            try
            {
                brandEntity.Name = dto.Name;
                _brandRepostory.Update(brandEntity);
                return new SuccessDataResult<BrandDto>(dto, "Marka başarıyla kaydedildi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<BrandDto>("Marka kaydedilirken bir hata oluştu: " + ex);
            }
        }

        public IResult DeleteBrand(int brandId)
        {
            var brandEntity = _brandRepostory.Get(b => b.Id == brandId);
            if (brandEntity == null)
            {
                return new ErrorResult($"#{brandId} id değerine ait bir marka bulunamadı.");
            }

            try
            {
                _brandRepostory.Delete(brandEntity);
                return new SuccessDataResult<BrandDto>("Marka başarıyla silindi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<BrandDto>("Marka silinirken bir hata oluştu: " + ex);
            }
        }

    }
}
