using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Shopping.DomainModel.Aggregates.Brands.Aggregates;
using Shopping.DomainModel.Aggregates.Categories.Aggregates;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.QueryModel.Implements.Brands;
using Shopping.QueryModel.QueryModels.Brands;
using Shopping.QueryService.Interfaces.Brands;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Brands
{
    public class BrandQueryService : IBrandQueryService
    {
        private readonly IReadOnlyRepository<Brand, Guid> _brandRepository;
        private readonly IReadOnlyRepository<Category, Guid> _categoryRepository;
        private readonly IReadOnlyRepository<Product, Guid> _productRepository;
        private readonly IReadOnlyRepository<CategoryBase, Guid> _categoryBaseRepository;
        public BrandQueryService(IReadOnlyRepository<Brand, Guid> brandRepository, IReadOnlyRepository<Category, Guid> categoryRepository, IReadOnlyRepository<Product, Guid> productRepository, IReadOnlyRepository<CategoryBase, Guid> categoryBaseRepository)
        {
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _categoryBaseRepository = categoryBaseRepository;
        }
        public IQueryable<BrandDto> GetAllQueryable()
        {
            var result = _brandRepository.AsQuery().ProjectTo<BrandDto>();
            return result;
        }
        public IBrandDto GetBrandById(Guid id)
        {
            var result = _brandRepository.Find(id);
            return result.ToDto();
        }
        public IList<IBrandDto> GetActiveCategoryBrands(Guid categoryId)
        {
            var category = _categoryBaseRepository.Find(categoryId);
            if (category == null)
            {
                throw new DomainException("دسته بندی یافت نشد");
            }
            var categoryProducts = _productRepository.AsQuery().Where(p => p.Category.Id == category.Id);
            var brands = categoryProducts.GroupBy(p => p.Brand).Where(item => item.Key.IsActive).Select(item => new
            {
                item.Key
            }).ToList().Select(item => item.Key.ToDto()).ToList();
            return brands;
        }
    }
}