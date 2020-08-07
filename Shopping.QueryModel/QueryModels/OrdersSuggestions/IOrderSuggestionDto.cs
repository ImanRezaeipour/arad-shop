using System;
using System.Collections.Generic;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.OrdersSuggestions.Abstract;
using Shopping.QueryModel.QueryModels.Persons.Shop;

namespace Shopping.QueryModel.QueryModels.OrdersSuggestions
{
    public interface IOrderSuggestionDto
    {
        Guid Id { get; set; }
        string OrderNumber { get; set; }
        long OrderId { get; set; }
        int Discount { get; set; }
        string Description { get; set; }
        IShopWithAddressDto Shop { get; set; }
        DateTime CreationTime { get; set; }
        decimal TotalPrice { get; set; }
        decimal DiscountTotalPrice { get; set; }
        int ShippingTime { get; set; }
        OrderSuggestionStatus OrderSuggestionStatus { get; set; }
        IOrderSuggestionItemTypeCountDto OrderSuggestionItemTypeCount { get; set; }
        IList<IOrderSuggestionItemBaseDto> OrderSuggestionItems { get; set; }
    }
}