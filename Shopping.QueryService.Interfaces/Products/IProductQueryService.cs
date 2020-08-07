using System;
using System.Linq;
using System.Threading.Tasks;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements.Products;
using Shopping.QueryModel.QueryModels.Products;

namespace Shopping.QueryService.Interfaces.Products
{
    public interface IProductQueryService
    {
        IQueryable<ProductDto> GetAll(Guid? brandId, Guid? categoryRootId, Guid? subCategoryId);
        IProductWithImageDto GetById(Guid id);
        MobilePagingResultDto<IProductDto> GetCategoryProducts(Guid categoryId, PagedInputDto pagedInput);
        MobilePagingResultDto<IProductDto> GetCategoryProductsSimilar(Guid productId, PagedInputDto pagedInput);
        MobilePagingResultDto<IProductDto> GetCategoryProductsSimilarWithName(Guid productId, string name, PagedInputDto pagedInput);
        MobilePagingResultDto<IProductDto> GetProductsWithNameAndBrandName(string text, PagedInputDto pagedInput);
        MobilePagingResultDto<IProductDto> GetProductsWithCategoryAndBrand(Guid categoryId, Guid brandId, PagedInputDto pagedInput);
        Task<MobilePagingResultDto<IProductDto>> GetProductsWithOtherCategoryAndBrand(Guid categoryId, Guid brandId, PagedInputDto pagedInput);
        Task<MobilePagingResultDto<IProductDto>> GetProductsWithCategoryAndOtherBrand(Guid categoryId, Guid brandId, PagedInputDto pagedInput);
    }
}