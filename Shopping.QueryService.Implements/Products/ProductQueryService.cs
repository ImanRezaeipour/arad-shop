using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Shopping.DomainModel.Aggregates.Categories.Aggregates;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements.Products;
using Shopping.QueryModel.QueryModels.Products;
using Shopping.QueryService.Interfaces.Products;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Products
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly IReadOnlyRepository<Product, Guid> _repository;
        private readonly IReadOnlyRepository<CategoryRoot, Guid> _categoryRepository;
        public ProductQueryService(IReadOnlyRepository<Product, Guid> repository, IReadOnlyRepository<CategoryRoot, Guid> categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }
        public IQueryable<ProductDto> GetAll(Guid? brandId, Guid? categoryRootId, Guid? subCategoryId)
        {
            var result = _repository.AsQuery();
            if (brandId != null)
            {
                result = result.Where(p => p.Brand.Id == brandId);
            }

            if (subCategoryId != null)
            {
                result = result.Where(p => p.Category.Id == subCategoryId);

            }
            else if (categoryRootId != null)
            {
                var category = _categoryRepository.AsQuery().SingleOrDefault(p => p.Id == categoryRootId);
                if (category == null) return result.ProjectTo<ProductDto>();
                {
                    var subCategory = category.SubCategories.Select(p => p.Id);
                    result = result.Where(p => subCategory.Contains(p.Category.Id));
                }
            }
            return result.ProjectTo<ProductDto>();
        }
        public IProductWithImageDto GetById(Guid id)
        {
            var result = _repository.Find(id).ToProductWithImageDto();
            return result;
        }
        public MobilePagingResultDto<IProductDto> GetCategoryProducts(Guid categoryId, PagedInputDto pagedInput)
        {
            var data = _repository.AsQuery().Where(p => p.Category.Id == categoryId);
            var result = data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToProductDto())
                .ToList();
            return new MobilePagingResultDto<IProductDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
        public MobilePagingResultDto<IProductDto> GetCategoryProductsSimilar(Guid productId, PagedInputDto pagedInput)
        {
            var product = _repository.AsQuery().SingleOrDefault(p => p.Id == productId);
            if (product == null)
            {
                throw new DomainException("محصول یافت نشد");
            }
            var data = _repository.AsQuery()
                .Where(p => p.Id != productId && p.IsActive && p.Category.Id == product.Category.Id);
            var result = data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToProductDto())
                .ToList();
            return new MobilePagingResultDto<IProductDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
        public MobilePagingResultDto<IProductDto> GetCategoryProductsSimilarWithName(Guid productId, string name, PagedInputDto pagedInput)
        {
            var product = _repository.AsQuery().SingleOrDefault(p => p.Id == productId);
            if (product == null)
            {
                throw new DomainException("محصول یافت نشد");
            }

            var VProduct = _repository.GetV_Brand_Product(name);
            var Ids = VProduct.Select(p => p.Id).ToList();
            var data = _repository.AsQuery().Where(p => Ids.Contains(p.Id) && p.IsActive && p.Id != product.Id
                                                        && p.Category.Id == product.Category.Id);

            var result = data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToProductDto())
                .ToList();
            return new MobilePagingResultDto<IProductDto>
            {
                Count = data.Count(),
                Result = result
            };

        }
        public MobilePagingResultDto<IProductDto> GetProductsWithNameAndBrandName(string text, PagedInputDto pagedInput)
        {
            text = CheckSerachValue(text);
            var VProduct = _repository.GetV_Brand_Product(text);
            var Ids = VProduct.Select(p => p.Id).ToList();
            var data = _repository.AsQuery().Where(p => Ids.Contains(p.Id) && p.IsActive);
            var result = data.OrderByDescending(p => p.CreationTime)
                 .Skip(pagedInput.Skip)
                 .Take(pagedInput.Count)
                 .ToList()
                 .Select(item => item.ToProductDto())
                 .ToList();
            return new MobilePagingResultDto<IProductDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
        public MobilePagingResultDto<IProductDto> GetProductsWithCategoryAndBrand(Guid categoryId, Guid brandId, PagedInputDto pagedInput)
        {
            var data = _repository.AsQuery()
                .Where(item => item.IsActive && item.Brand.Id == brandId && item.Category.Id == categoryId);
            var result = data
                .OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(item => item.ToProductDto())
                .ToList();
            return new MobilePagingResultDto<IProductDto>
            {
                Count = data.Count(),
                Result = result
            };
        }

        public async Task<MobilePagingResultDto<IProductDto>> GetProductsWithOtherCategoryAndBrand(Guid categoryId, Guid brandId, PagedInputDto pagedInput)
        {
            var data = _repository.AsQuery()
                .Where(item => item.IsActive && item.Brand.Id == brandId && item.Category.Id != categoryId);
            var result = await data
                .OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToListAsync();
            return new MobilePagingResultDto<IProductDto>
            {
                Count = data.Count(),
                Result = result.Select(item => item.ToProductDto())
                    .ToList()
            };
        }

        public async Task<MobilePagingResultDto<IProductDto>> GetProductsWithCategoryAndOtherBrand(Guid categoryId, Guid brandId, PagedInputDto pagedInput)
        {
            var data = _repository.AsQuery()
                .Where(item => item.IsActive && item.Brand.Id != brandId && item.Category.Id == categoryId);
            var result = await data
                .OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToListAsync();
            return new MobilePagingResultDto<IProductDto>
            {
                Count = data.Count(),
                Result = result.Select(item => item.ToProductDto())
                    .ToList()
            };
        }

        private string CheckSerachValue(string name)
        {
            var result = name.TrimEnd();
            result = result.TrimStart();
            return result;
        }
    }
}