using System;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.Factors;
using Shopping.QueryModel.QueryModels.Factors.ShipmentStates;

namespace Shopping.QueryModel.Implements.Factors
{
    public class FactorDto: IFactorDto
    {
        public FactorDto()
        {
            
        }
        public long Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long OrderId { get; set; }
        public Guid OrderSuggestionId { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal SystemDiscountPrice { get; set; }
        public FactorState FactorState { get; set; }
        public ShipmentState ShipmentState { get; set; }
        public int? TimeLeft { get; set; }
        public int? ShipmentTimeLeft { get; set; }
        public int FactoRawCount { get; set; }
        public int ShippingTime { get; set; }
        public IShipmentStateBaseDto ShipmentStateBase { get; set; }
        public bool IsExpired { get; set; }
    }
}