using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.DomainModel.Aggregates.Categories.Aggregates;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.Linq;
using Shopping.QueryModel.Implements.Categories;
using Shopping.QueryModel.QueryModels.Categories;
using Shopping.QueryModel.QueryModels.Categories.Abstract;
using Shopping.QueryService.Implements.Products;
using Shopping.QueryService.Interfaces.Categories;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Categories
{
    public class CategoryQueryService : ICategoryQueryService
    {
        private readonly IReadOnlyRepository<CategoryBase, Guid> _categoryRepository;
        private readonly IReadOnlyRepository<Product, Guid> _productRepository;
        public CategoryQueryService(IReadOnlyRepository<CategoryBase, Guid> categoryRepository, IReadOnlyRepository<Product, Guid> productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public IEnumerable<ICategoryBaseWithSubCategoriesDto> GetAll()
        {
            var result = _categoryRepository.AsQuery().OfType<CategoryRoot>().OrderBy(p => p.Order).ToList()
                .Select(item => item.ToCategoryBaseWithSubCategoriesDto());
            return result;
        }
        public ICategoryBaseWithImageDto GetCategoryById(Guid id)
        {
            var result = _categoryRepository.Find(id);
            return result.ToCategoryBaseWithImageDto();
        }
        public IEnumerable<ICategoryBaseWithImageDto> GetCategoryRoots()
        {
            var result = _categoryRepository.AsQuery().OfType<CategoryRoot>().OrderBy(p => p.Order).ToList()
                .Select(item => item.ToCategoryBaseWithImageDto()).ToList();
            return result;
        }
        public IList<ICategoryBaseWithImageDto> GetActiveBrandSubCategories(Guid categoryId, Guid brandId)
        {
            var category = _categoryRepository.Find(categoryId);
            if (category == null)
            {
                throw new DomainException("دسته بندی یافت نشد");
            }
            var subCategories = category.SubCategories.Where(item => item.IsActive)
                .Select(item => item.ToCategoryBaseWithImageDto());
            var result = new List<ICategoryBaseWithImageDto>();
            foreach (var item in subCategories)
            {
                if (_productRepository.AsQuery().Any(p => p.Category.Id == item.Id && p.Brand.Id == brandId))
                {
                    result.Add(item);
                }
            }
            return result.OrderBy(p => p.Order).ToList();
        }

        public IEnumerable<ICategoryBaseWithImageDto> GetActiveCategoryRoots()
        {
            var result = _categoryRepository.AsQuery().OfType<CategoryRoot>()
                .Where(item => item.IsActive && item.SubCategories.Any()).ToList()
                .Select(item => item.ToCategoryBaseWithImageDto()).OrderBy(p => p.Order).ToList();
            return result;
        }
        public IEnumerable<ICategoryBaseWithImageDto> GetActiveSubCategories(Guid categoryId)
        {
            var categoryRoot = _categoryRepository.Find(categoryId);
            if (categoryRoot == null)
            {
                throw new DomainException("دسته بندی یافت نشد");
            }
            var subCategories = categoryRoot.SubCategories.Where(item => item.IsActive);
            return subCategories.OrderBy(p => p.Order).ToList().Select(item => item.ToCategoryBaseWithImageDto());
        }
        public IEnumerable<ICategoryWithProductDto> GetCategoryRootsWithProducts()
        {
            var categoryRoots = _categoryRepository.AsQuery().OfType<CategoryRoot>()
                .Where(item => item.IsActive).ToList();
            var dto = new List<ICategoryWithProductDto>();
            foreach (var categoryRoot in categoryRoots)
            {
                var temp = new CategoryWithProductDto
                {
                    Id = categoryRoot.Id,
                    IsActive = categoryRoot.IsActive,
                    Name = categoryRoot.Name,
                    IsRoot = true,
                    CategoryImage = new CategoryImageDto
                    {
                        TopPageCatImage = categoryRoot.CategoryImage.TopPageCatImage,
                        FullMainCatImage = categoryRoot.CategoryImage.FullMainCatImage,
                        MainCatImage = categoryRoot.CategoryImage.MainCatImage
                    },
                    Description = categoryRoot.Description,
                    Order = categoryRoot.Order
                };
                var products = _productRepository.AsQuery().Where(item => item.IsActive);
                var subCategories = _categoryRepository.AsQuery().Where(item => item.Id == categoryRoot.Id)
                    .SelectMany(i => i.SubCategories).Where(item => item.IsActive);
                var categoryRootProducts = _productRepository.AsQuery().Where(item => item.IsActive && item.Category.Id == categoryRoot.Id);
                var subCategoryProducts = products.Join(subCategories, p => p.Category.Id, s => s.Id,
                    (pro, cat) => new
                    {
                        Product = pro,
                    }).Select(item => item.Product);
                var union = categoryRootProducts.Union(subCategoryProducts)
                    .OrderByDescending(item => item.CreationTime).Take(20).ToList();
                temp.Products = union.Select(item => item.ToProductDto()).ToList();
                if (temp.Products.Any())
                {
                    dto.Add(temp);
                }
            }
            return dto.OrderBy(p => p.Order);
        }
        public IEnumerable<ICategoryWithProductDto> GetCategoriesWithProducts(Guid categoryRootId, Guid? brandId)
        {
            var categories = _categoryRepository.AsQuery().Where(item => item.Id == categoryRootId)
                .SelectMany(item => item.SubCategories).Where(item => item.IsActive);
            var dto = new List<ICategoryWithProductDto>();
            foreach (var category in categories.ToList())
            {
                var temp = new CategoryWithProductDto
                {
                    Id = category.Id,
                    IsActive = category.IsActive,
                    Name = category.Name,
                    IsRoot = true,
                    CategoryImage = new CategoryImageDto
                    {
                        TopPageCatImage = category.CategoryImage.TopPageCatImage,
                        FullMainCatImage = category.CategoryImage.FullMainCatImage,
                        MainCatImage = category.CategoryImage.MainCatImage
                    },
                    Description = category.Description,
                    Order = category.Order
                };
                var products = _productRepository.AsQuery()
                    .Where(item => item.IsActive && item.Category.Id == category.Id)
                    .WhereIf(brandId != null, item => item.Brand.Id == brandId)
                    .OrderByDescending(item => item.CreationTime).Take(20).ToList();
                temp.Products = products.Select(item => item.ToProductDto()).ToList();
                if (temp.Products.Any())
                {
                    dto.Add(temp);
                }
            }
            return dto.OrderBy(p => p.Order);
        }
    }
}