using System;
using System.Linq;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements.ProductsSuggestions;
using Shopping.QueryModel.QueryModels.ProductsSuggestions.ShopProductsSuggestion;

namespace Shopping.QueryService.Interfaces.ProductsSuggestion
{
    public interface IShopProductsSuggestionQueryService
    {
        IShopProductSuggestionDto GetById(Guid id);
        IQueryable<ShopProductSuggestionDto> GetAll();
        MobilePagingResultDto<IShopProductSuggestionDto> GetByUserId(Guid userId, PagedInputDto pagedInput);
    }
}