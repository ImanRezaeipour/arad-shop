using System;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.ShopOrderLogs.Events
{
    public class CreateShopOrderLogEvent: IDomainEvent
    {
        public CreateShopOrderLogEvent(Shop shop, OrderBase order)
        {
            Shop = shop;
            Order = order;
            DomainEventDate=DateTime.Now;
        }
        public Shop Shop { get; set; }
        public OrderBase Order { get; set; }
        public DateTime DomainEventDate { get; set; }
    }
}