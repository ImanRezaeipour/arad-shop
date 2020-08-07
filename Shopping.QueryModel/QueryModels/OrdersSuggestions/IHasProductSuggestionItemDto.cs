using Shopping.QueryModel.QueryModels.OrdersSuggestions.Abstract;

namespace Shopping.QueryModel.QueryModels.OrdersSuggestions
{
    public interface IHasProductSuggestionItemDto : IOrderSuggestionItemBaseDto
    {
        int Quantity { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        bool IsQuantityChanged { get; set; }
    }
}