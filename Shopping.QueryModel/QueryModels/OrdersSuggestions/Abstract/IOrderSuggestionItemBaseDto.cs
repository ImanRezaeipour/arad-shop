using System;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.Orders.Items;

namespace Shopping.QueryModel.QueryModels.OrdersSuggestions.Abstract
{
    public interface IOrderSuggestionItemBaseDto
    {
        Guid Id { get; set; }
        IOrderItemDto OrderItem { get; set; }
        OrderSuggestionItemType OrderSuggestionItemType { get; set; }
    }
}