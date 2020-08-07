using System;
using System.Linq;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements.ProductsSuggestions;
using Shopping.QueryModel.QueryModels.ProductsSuggestions.CustomerProductsSuggestion;

namespace Shopping.QueryService.Interfaces.ProductsSuggestion
{
    public interface ICustomerProductsSuggestionQueryService
    {
        ICustomerProductSuggestionDto GetById(Guid id);
        IQueryable<CustomerProductSuggestionDto> GetAll();
        MobilePagingResultDto<ICustomerProductSuggestionDto> GetByUserId(Guid userId, PagedInputDto pagedInput);
    }
}