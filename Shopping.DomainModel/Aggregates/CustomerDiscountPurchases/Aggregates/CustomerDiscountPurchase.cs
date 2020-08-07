using System;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.CustomerDiscountPurchases.Aggregates
{
    public class CustomerDiscountPurchase : AggregateRoot
    {
        protected CustomerDiscountPurchase() { }
        public CustomerDiscountPurchase(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
            Count = 1;
        }
        public Guid CustomerId { get; private set; }
        public int Count { get; private set; }
        public byte[] RowVersion { get; set; }
        public void AddCount() => ++Count;
        public override void Validate()
        {
        }
    }
}