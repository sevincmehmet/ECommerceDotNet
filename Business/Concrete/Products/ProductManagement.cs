using Business.Abstract.Products;
using Business.DTOs.Product;
using Business.Extensions;
using DataAccess.Abstract.Products;
using DataAccess.Abstract.Users;
using Entities.Concrete.Products;
using Infrastructure.Result.Abstract;
using Infrastructure.Result.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete.Products
{
    public class ProductManagement(IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepostory brandRepostory, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : IProductService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        ICategoryRepository _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        IBrandRepostory _brandRepository = brandRepostory ?? throw new ArgumentNullException(nameof(brandRepostory));
        IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));



        public List<CategoriyDto> GetFormattedAllCategory()
        {
            return _categoryRepository.GetList()
                .Select(c => new CategoriyDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                    ParentCategoryId = c.ParentCategoryId,
                    Level = c.Level,
                    Childrens = new List<CategoriyDto>()
                }).ToList();
        }

        public IDataResult<List<ProductDto>> GetAll()
        {
            List<Product> entityProduct = _productRepository.GetList();
            var dtoProduct = entityProduct.Select(product => new ProductDto
            {
                Id = product.Id,
                BrandName = product.Brand.Name,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                CreatedDate = product.CreatedDate,
                Category = product.Category,
            }
            ).ToList();

            return new SuccessDataResult<List<ProductDto>>(dtoProduct, "Ürünler başarıyla listelendi");
        }

        public IDataResult<ProductDto> GetById(int productId)
        {
            Product? entityProduct = _productRepository.Get(p => p.Id == productId);
            if (entityProduct == null)
            {
                return new ErrorDataResult<ProductDto>(null, "Ürün bulunamadı");
            }
            var dtoProduct = new ProductDto
            {
                Id = productId,
                BrandName = entityProduct.Brand.Name,
                Name = entityProduct.Name,
                Description = entityProduct.Description,
                UnitPrice = entityProduct.UnitPrice,
                CreatedDate = entityProduct.CreatedDate,
                Category = entityProduct.Category,
            };
            return new SuccessDataResult<ProductDto>(dtoProduct, "Ürün başarıyla getirildi");
        }

        public IDataResult<List<ProductDto>> GetProductsByCategoryId(int categoryId)
        {
            try
            {

                var allCategories = GetFormattedAllCategory();
                var category = _categoryRepository.Get(c => c.Id == categoryId);
                if (category == null)
                {
                    return new ErrorDataResult<List<ProductDto>>($"#{categoryId} id değerine göre bir kategori bulunamadı");
                }
                // 2. Bu ID ve altındaki tüm kategorilerin ID listesini al
                var categoryIds = allCategories.GetSelfAndChildIds(category.Id);
                if (!categoryIds.Any())
                {
                    return new ErrorDataResult<List<ProductDto>>("Kategori hiyerarşisi çözülemedi.");
                }

                // 3. Repository'den bu ID listesine sahip ürünleri çek
                // Repository'deki GetList metodun Expression alıyorsa:
                var products = _productRepository.GetList(p => categoryIds.Contains(p.CategoryId));

                if (products == null || !products.Any())
                {
                    return new SuccessDataResult<List<ProductDto>>(new List<ProductDto>(), "Bu kategoride ve alt kategorilerinde ürün bulunamadı.");
                }

                var productList = products.Select(ep => new ProductDto
                {
                    Id = ep.Id,
                    BrandName = ep.Brand?.Name ?? "Markasız",
                    Name = ep.Name,
                    UnitPrice = ep.UnitPrice,
                    UnitsInStock = ep.UnitsInStock,
                    Category = ep.Category,
                }).ToList();

                return new SuccessDataResult<List<ProductDto>>(productList, "Ürünler alt kategoriler dahil başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductDto>>("İşlem sırasında bir hata oluştu: " + ex.Message);
            }
        }

        public IDataResult<List<ProductDto>> GetProductsByCategorySlug(string categorySlug)
        {
            try
            {

                var allCategories = GetFormattedAllCategory();
                var category = _categoryRepository.Get(c => c.Slug == categorySlug);
                if (category == null)
                {
                    return new ErrorDataResult<List<ProductDto>>($"#{categorySlug} slug değerine göre bir kategori bulunamadı");
                }
                // 2. Bu ID ve altındaki tüm kategorilerin ID listesini al
                var categoryIds = allCategories.GetSelfAndChildIds(category.Id);
                if (!categoryIds.Any())
                {
                    return new ErrorDataResult<List<ProductDto>>("Kategori hiyerarşisi çözülemedi.");
                }


                var products = _productRepository.GetList(p => categoryIds.Contains(p.CategoryId));

                if (products == null || !products.Any())
                {
                    return new SuccessDataResult<List<ProductDto>>(new List<ProductDto>(), "Bu kategoride ve alt kategorilerinde ürün bulunamadı.");
                }

                var productList = products.Select(ep => new ProductDto
                {
                    Id = ep.Id,
                    BrandName = ep.Brand?.Name ?? "Markasız",
                    Name = ep.Name,
                    UnitPrice = ep.UnitPrice,
                    UnitsInStock = ep.UnitsInStock,
                    Category = ep.Category,
                }).ToList();

                return new SuccessDataResult<List<ProductDto>>(productList, "Ürünler alt kategoriler dahil başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductDto>>("İşlem sırasında bir hata oluştu: " + ex.Message);
            }
        }

        // < ---- ADMIN - ISTEKLERI ---- >
        public IDataResult<ProductAddDto> CreateProduct(ProductAddDto dto)
        {

            var Category = _categoryRepository.Get(c => c.Id == dto.CategoryId);
            var Brand = _brandRepository.Get(b => b.Id == dto.BrandId);
            if (Category == null)
            {
                return new ErrorDataResult<ProductAddDto>($" #{dto.CategoryId} id değerine ait bir kategori bulunamadı.");

            }
            if (Brand == null)
            {
                return new ErrorDataResult<ProductAddDto>($" #{dto.BrandId} id değerine ait bir marka bulunamadı.");

            }
            if (string.IsNullOrEmpty(dto.Name) || dto.Name.Length < 2)
            {
                return new ErrorDataResult<ProductAddDto>("Ürün adı boş veya 2 karakterden az olamaz");
            }

            if (dto.UnitPrice < 0)
            {
                dto.UnitPrice = 0;
            }
            if (dto.UnitsInStock < 0)
            {
                dto.UnitsInStock = 0;
            }

            var newProduct = new Product
            {
                CategoryId = dto.CategoryId,
                BrandId = dto.BrandId,
                Name = dto.Name,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice,
                UnitsInStock = dto.UnitsInStock,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedDate = DateTime.UtcNow
            };

            try
            {
                _productRepository.Add(newProduct);
                var newProductDto = new ProductAddDto
                {
                    Id = newProduct.Id,
                    CategoryId = newProduct.CategoryId,
                    BrandId = newProduct.BrandId,
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    UnitPrice = newProduct.UnitPrice,
                    UnitsInStock = newProduct.UnitsInStock
                };

                return new SuccessDataResult<ProductAddDto>(newProductDto, "Ürün başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ProductAddDto>("Ürün oluşturulurken bir hata oluştu: " + ex.Message);
            }

        }

        public IDataResult<ProductUpdateDto> UpdateProduct(int productId, ProductUpdateDto dto)
        {
            var productEntity = _productRepository.Get(p => p.Id == productId);
            if (productEntity == null)
            {
                return new ErrorDataResult<ProductUpdateDto>($" #{productId} id değerine ait bir ürün bulunamadı.");
            }

            var Category = _categoryRepository.Get(c => c.Id == dto.CategoryId);
            var Brand = _brandRepository.Get(b => b.Id == dto.BrandId);

            if (Category == null)
            {
                return new ErrorDataResult<ProductUpdateDto>($" #{dto.CategoryId} id değerine ait bir kategori bulunamadı.");
            }


            if (Brand == null)
            {
                return new ErrorDataResult<ProductUpdateDto>($" #{dto.BrandId} id değerine ait bir marka bulunamadı.");
            }

            if (dto.Name == null || dto.Name.Trim() == "")
            {
                dto.Name = productEntity.Name;
            }


            if (dto.Description == null || dto.Description.Trim() == "")
            {
                dto.Description = productEntity.Description;
            }

            else if (dto.Name.Length < 2)
            {
                return new ErrorDataResult<ProductUpdateDto>("Ad en az 2 karakter olmalıdır.");
            }

            if (dto.CreatedByUserId != null || dto.CreatedByUserId > 0)
            {
                var dtoUser = _userRepository.Get(u => u.Id == dto.CreatedByUserId);

                if (dtoUser == null)
                {
                    return new ErrorDataResult<ProductUpdateDto>($"{dto.CreatedByUserId} id değerine göre bir user bulunamadı.");
                }


            }

            if (dto.UnitPrice == null && dto.UnitPrice < 0)
            {
                dto.UnitPrice = productEntity.UnitPrice;
            }

            if (dto.UnitPrice == null && dto.UnitsInStock < 0)
            {
                dto.UnitsInStock = productEntity.UnitsInStock;
            }

            try
            {
                productEntity.CategoryId = dto.CategoryId;
                productEntity.BrandId = dto.BrandId;
                productEntity.Name = dto.Name;
                productEntity.Description = dto.Description;
                productEntity.UnitsInStock = dto.UnitsInStock;
                productEntity.UnitPrice = dto.UnitPrice;
                productEntity.CreatedByUserId = dto.CreatedByUserId;

                _productRepository.Update(productEntity);

                return new SuccessDataResult<ProductUpdateDto>(dto, "Ürün başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ProductUpdateDto>("Ürün güncellenirken bir hata oluştu");
            }
        }

        public IResult DeleteProduct(int productId)
        {
            var product = _productRepository.Get(p => p.Id == productId);

            if (product == null)
            {
                return new ErrorResult($" #{productId} id değerine ait bir ürün bulunamadı.");
            }

            //bool canDelete = currentUserPermissions.Contains("Product.Delete");


            _productRepository.Delete(product);
            return new SuccessResult("Ürün başarıyla silindi.");
        }


        // < --- GİRİŞ YAPAN KULLANICI İSTEKLERİ ---- >
        public IDataResult<ProductMeAddDto> CreateMyProduct(ProductMeAddDto dto)
        {
            throw new NotImplementedException();
        }

        public IDataResult<ProductUpdateDto> UpdateMyProduct(ProductUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public IResult DeleteMyProduct(int productId)
        {
            throw new NotImplementedException();
        }


    }
}