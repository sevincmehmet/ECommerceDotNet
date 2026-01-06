using Business.Abstract.Products;
using Business.DTOs.Product;
using Business.Extensions;
using DataAccess.Abstract.Products;
using Entities.Concrete.Products;
using Infrastructure.Result.Abstract;
using Infrastructure.Result.Concrete;

namespace Business.Concrete.Products
{
    public class CategoryManagement(ICategoryRepository categoryRepository, IProductRepository productRepository) : ICategoryService
    {
        ICategoryRepository _categoriyRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

        public List<CategoriyDto> GetFormattedAllCategory()
        {
            return _categoriyRepository.GetList()
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

        public IDataResult<List<CategoriyDto>> GetAll()
        {
            var allCategories = GetFormattedAllCategory();
            var treeStructure = allCategories.ToTree(c => c.Level == 1);

            return new SuccessDataResult<List<CategoriyDto>>(treeStructure, "Kategori listesi başarıyla getirildi.");
        }

        public IDataResult<CategoriyDto> GetById(int id)
        {
            var allCategories = GetFormattedAllCategory();


            var categoryTree = allCategories.ToTree(c => c.Id == id).FirstOrDefault();

            if (categoryTree == null)
            {
                return new ErrorDataResult<CategoriyDto>($"#{id} id değerine ait bir kategori bulunamadı.");
            }

            return new SuccessDataResult<CategoriyDto>(categoryTree, "Kategori ve alt dalları başarıyla getirildi.");
        }

        public IResult DeleteCategory(int id)
        {
            var categoryEntity = _categoriyRepository.Get(c => c.Id == id);
            var categoryChild = _categoriyRepository.GetList().Where(c => c.ParentCategoryId == id).ToList();
            var categoryProduct = _productRepository.GetList().Where(c => c.CategoryId == id).ToList();

            if (categoryChild.Count > 0)
            {
                return new ErrorResult($"Bu kategoriyi silmek için önce bu kategorinin altındaki tüm alt kategorilerini ({categoryChild.Count} adet) silmelisin.");
            }

            if (categoryProduct.Count > 0)
            {
                return new ErrorResult($"Bu kategoriyi silmek için önce kategori içerssindeki tüm ürünleri ({categoryProduct.Count} adet) silmelisin.");
            }

            try
            {
                _categoriyRepository.Delete(categoryEntity);
                return new SuccessResult("Kategori başarıyla silindi");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Ürün silinirken bir hata oluştu: " + ex);
            }
        }


        public IDataResult<List<Product>> GetCategoryProductBySlug(string slug)
        {
            throw new NotImplementedException();
        }
        public IDataResult<ProductCategoryDto> GetCategoryProductById(int productId)
        {
            var product = _productRepository.Get(p => p.Id == productId);

            if (product == null)
                return new ErrorDataResult<ProductCategoryDto>($"{productId} numaralı ürün bulunamadı.");

            if (product.CategoryId == 0)
                return new ErrorDataResult<ProductCategoryDto>("Bu ürün herhangi bir kategoriye bağlı değil.");

            var currentCategory = _categoriyRepository.Get(c => c.Id == product.CategoryId);
            if (currentCategory == null)
                return new ErrorDataResult<ProductCategoryDto>("Ürünün bağlı olduğu kategori veritabanında bulunamadı.");

            var categoryList = new List<Category>();
            var tempCategory = currentCategory;

            var resultProductcategories = new ProductCategoryDto
            {
                Categoriys = new List<CategoriyDto>(),
                BreadCrumb = new List<string>(),
                Slug = currentCategory.Slug
            };

            // 1. Üste doğru yolu topla
            while (tempCategory != null)
            {
                categoryList.Add(tempCategory);
                if (tempCategory.ParentCategoryId == null || tempCategory.Level == 1) break;
                tempCategory = _categoriyRepository.Get(c => c.Id == tempCategory.ParentCategoryId);
            }

            // 2. Hiyerarşiyi ve Breadcrumb'ı kur
            CategoriyDto rootDto = null;
            CategoriyDto lastDto = null;

            for (int i = categoryList.Count - 1; i >= 0; i--)
            {
                var cat = categoryList[i];

                // Sadece isimleri Breadcrumb'a ekliyoruz
                resultProductcategories.BreadCrumb.Add(cat.Name);

                var currentDto = new CategoriyDto
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Slug = cat.Slug,
                    ParentCategoryId = cat.ParentCategoryId,
                    Level = cat.Level,
                    Childrens = new List<CategoriyDto>()
                };

                if (rootDto == null) rootDto = currentDto;
                else lastDto.Childrens.Add(currentDto);

                lastDto = currentDto;
            }

            if (rootDto != null)
                resultProductcategories.Categoriys.Add(rootDto);

            return new SuccessDataResult<ProductCategoryDto>(resultProductcategories, "Ürünün kategori ağacı başarıyla getirildi.");
        }

        public IDataResult<CategoriyDto> AddCategory(CategoriyAddDto dto)
        {
            Category categoryParent = null;

            if (dto.ParentCategoryId != null)
            {
                categoryParent = _categoriyRepository.Get(c => c.Id == dto.ParentCategoryId);
                if (categoryParent == null)
                {
                    return new ErrorDataResult<CategoriyDto>($"Üst kategori olarak eklemek istediğiniz, #{dto.ParentCategoryId} id değerine göre bir kategori bulunamadı.");
                }
                if (categoryParent.Level > 2)
                {
                    return new ErrorDataResult<CategoriyDto>("En alt grubun altına grup ekleyemezsiniz.");
                }
            }
            if (dto.Name.Length < 2)
            {
                return new ErrorDataResult<CategoriyDto>("Kategori adı 2 harften az olamaz");
            }

            var isDuplicate = _categoriyRepository.Get(c =>
                c.ParentCategoryId == dto.ParentCategoryId &&
                c.Name.ToLower().Trim() == dto.Name.ToLower().Trim());

            if (isDuplicate != null)
            {
                return new ErrorDataResult<CategoriyDto>($"{dto.Name} adında bir kategori bu seviyede zaten mevcut.");
            }

            try
            {
                // 1. DTO'yu Entity'ye çevir (Mapping)
                var newCategory = new Category
                {
                    Name = dto.Name,
                    ParentCategoryId = dto.ParentCategoryId,
                    Level = categoryParent == null ? 1 : categoryParent.Level + 1
                };

                var allCategories = _categoriyRepository.GetList();
                newCategory.Slug = newCategory.CreateSlugPath(allCategories);
                _categoriyRepository.Add(newCategory);

                CategoriyDto newCategoriyDto = new CategoriyDto
                {
                    Id = newCategory.Id,
                    Name = newCategory.Name,
                    ParentCategoryId = newCategory.ParentCategoryId,
                    Level = newCategory.Level,
                    Slug = newCategory.Slug,
                };


                return new SuccessDataResult<CategoriyDto>(newCategoriyDto, "Kategori başarıyla eklendi");

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CategoriyDto>("Kategori eklenirken bir hata oluştu: " + ex);
            }
        }

        public IDataResult<CategoriyDto> UpdateCategory(int id, CategoriyUpdateDto dto)
        {
            var categoryEntity = _categoriyRepository.Get(c => c.Id == id);
            if (categoryEntity == null)
            {
                return new ErrorDataResult<CategoriyDto>($"#{id} id değerine göre bir kategori bulunamadı.");
            }
            if (dto.Name == categoryEntity.Name)
            {
                return new ErrorDataResult<CategoriyDto>("Veriler tamamen aynı");
            }
            if (dto.Name.Length < 2)
            {
                return new ErrorDataResult<CategoriyDto>("Kategori adı 2 harften az olamaz");
            }

            var isDuplicate = _categoriyRepository.Get(c =>
                c.ParentCategoryId == categoryEntity.ParentCategoryId &&
                c.Name.ToLower().Trim() == dto.Name.ToLower().Trim());

            if (isDuplicate != null)
            {
                return new ErrorDataResult<CategoriyDto>($"{dto.Name} adında bir kategori bu seviyede zaten mevcut.");
            }
            try
            {

                categoryEntity.Name = dto.Name;

                var allCategories = _categoriyRepository.GetList();
                categoryEntity.Slug = categoryEntity.CreateSlugPath(allCategories);


                _categoriyRepository.Update(categoryEntity);

                var categoryDto = new CategoriyDto
                {
                    Id = id,
                    Name = categoryEntity.Name,
                    Slug = categoryEntity.Slug,
                    Level = categoryEntity.Level,
                    ParentCategoryId = categoryEntity.ParentCategoryId,
                };

                return new SuccessDataResult<CategoriyDto>(categoryDto, "Kategori başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CategoriyDto>("Kategori güncellenirken bir hata oluştu");
            }

        }
    }
}
