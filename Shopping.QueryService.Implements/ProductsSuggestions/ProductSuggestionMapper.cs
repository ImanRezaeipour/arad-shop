using AutoMapper;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates.Abstrct;
using Shopping.QueryModel.QueryModels.ProductsSuggestions.Abstract;
using Shopping.QueryModel.QueryModels.ProductsSuggestions.CustomerProductsSuggestion;
using Shopping.QueryModel.QueryModels.ProductsSuggestions.ShopProductsSuggestion;

namespace Shopping.QueryService.Implements.ProductsSuggestions
{
    public static class ProductSuggestionMapper
    {
        public static IProductSuggestionDto ToDto(this ProductSuggestion src)
        {
            return Mapper.Map<IProductSuggestionDto>(src);
        }
        public static IShopProductSuggestionDto ToDto(this ShopProductSuggestion src)
        {
            return Mapper.Map<IShopProductSuggestionDto>(src);
        }
        public static ICustomerProductSuggestionDto ToDto(this CustomerProductSuggestion src)
        {
            return Mapper.Map<ICustomerProductSuggestionDto>(src);
        }
        public static ICustomerProductSuggestionDto ToDtoCustomerProductSuggestion(this ProductSuggestion src)
        {
            return Mapper.Map<ICustomerProductSuggestionDto>(src);
        }
    }
}