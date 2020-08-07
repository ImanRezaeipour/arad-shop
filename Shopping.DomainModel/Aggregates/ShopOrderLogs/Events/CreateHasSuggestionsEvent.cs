using System;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Aggregates;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.ShopOrderLogs.Events
{
    public class CreateHasSuggestionsEvent : IDomainEvent
    {
        public CreateHasSuggestionsEvent(long orderId, Guid shopId, OrderSuggestion orderSuggestion)
        {
            DomainEventDate = DateTime.Now;
            OrderId = orderId;
            ShopId = shopId;
            OrderSuggestion = orderSuggestion;
        }
        public DateTime DomainEventDate { get; set; }
        public long OrderId { get; set; }
        public Guid ShopId { get; set; }
        public OrderSuggestion OrderSuggestion { get; set; }
    }
}