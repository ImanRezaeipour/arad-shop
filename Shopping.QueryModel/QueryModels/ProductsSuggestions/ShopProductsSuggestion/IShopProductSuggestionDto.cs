using Shopping.QueryModel.QueryModels.ProductsSuggestions.Abstract;

namespace Shopping.QueryModel.QueryModels.ProductsSuggestions.ShopProductsSuggestion
{
    public interface IShopProductSuggestionDto: IProductSuggestionDto
    {
        string ShopName { get; set; }
    }
}