using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.CustomerDiscountPurchases.Aggregates;
using Shopping.DomainModel.Aggregates.CustomerDiscountPurchases.Events;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.CustomerDiscountPurchases.EventHandlers
{
    public class CustomerDiscountPurchaseEventHandler : IDomainEventHandler<AddCustomerDiscountPurchaseEvent>
    {
        private readonly IRepository<CustomerDiscountPurchase> _repository;
        public CustomerDiscountPurchaseEventHandler(IRepository<CustomerDiscountPurchase> repository)
        {
            _repository = repository;
        }
        public void Handle(AddCustomerDiscountPurchaseEvent @event)
        {
            var purchase = _repository.AsQuery().FirstOrDefault(p => p.CustomerId == @event.CustomerId);
            if (purchase != null)
            {
                purchase.AddCount();
            }
            else
            {
                purchase = new CustomerDiscountPurchase(Guid.NewGuid(), @event.CustomerId);
                _repository.Add(purchase);
            }
        }
    }
}