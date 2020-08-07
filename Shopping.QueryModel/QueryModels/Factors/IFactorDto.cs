using System;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.Factors.ShipmentStates;

namespace Shopping.QueryModel.QueryModels.Factors
{
    public interface IFactorDto
    {
        long Id { get; set; }
        DateTime CreationTime { get; set; }
        long OrderId { get; set; }
        Guid OrderSuggestionId { get; set; }
        decimal Price { get; set; }
        int Discount { get; set; }
        decimal DiscountPrice { get; set; }
        decimal SystemDiscountPrice { get; set; }
        FactorState FactorState { get; set; }
        ShipmentState ShipmentState { get; set; }
        int? TimeLeft { get; set; }
        int? ShipmentTimeLeft { get; set; }
        int FactoRawCount { get; set; }
        int ShippingTime { get; set; }
        IShipmentStateBaseDto ShipmentStateBase { get; set; }
        bool IsExpired { get; set; }
    }
}