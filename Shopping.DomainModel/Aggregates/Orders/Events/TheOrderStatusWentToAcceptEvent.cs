using System;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.Orders.Events
{
    public class TheOrderStatusWentToAcceptEvent : IDomainEvent
    {
        public TheOrderStatusWentToAcceptEvent(OrderBase orderBase)
        {
            OrderBase = orderBase;
            DomainEventDate = DateTime.Now;
        }
        public OrderBase OrderBase { get; set; }
        public DateTime DomainEventDate { get; }
    }
}