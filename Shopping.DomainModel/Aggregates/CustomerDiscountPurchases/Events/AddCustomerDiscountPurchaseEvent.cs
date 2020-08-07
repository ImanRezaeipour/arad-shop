using System;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.CustomerDiscountPurchases.Events
{
    public class AddCustomerDiscountPurchaseEvent : IDomainEvent
    {
        public AddCustomerDiscountPurchaseEvent(Guid customerId)
        {
            CustomerId = customerId;
            DomainEventDate = DateTime.Now;
        }
        public Guid CustomerId { get; }
        public DateTime DomainEventDate { get; }
    }
}