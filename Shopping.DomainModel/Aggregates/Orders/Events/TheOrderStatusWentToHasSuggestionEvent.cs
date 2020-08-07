using System;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.Orders.Events
{
    public class TheOrderStatusWentToHasSuggestionEvent : IDomainEvent
    {
        public TheOrderStatusWentToHasSuggestionEvent(OrderBase order)
        {
            Order = order;
            DomainEventDate = DateTime.Now;
        }
        public OrderBase Order { get; set; }
        public DateTime DomainEventDate { get; }
    }
}