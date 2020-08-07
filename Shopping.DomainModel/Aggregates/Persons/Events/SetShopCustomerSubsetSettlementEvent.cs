using System;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.Persons.Events
{
    public class SetShopCustomerSubsetSettlementEvent : IDomainEvent
    {
        public SetShopCustomerSubsetSettlementEvent(Shop shop)
        {
            Shop = shop;
            DomainEventDate = DateTime.Now;
        }
        public Shop Shop { get; private set; }
        public DateTime DomainEventDate { get; private set; }
    }
}