using Shopping.DomainModel.Aggregates.Persons.Events;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.Persons.EventHandlers
{
    public class ShopEventHandler:IDomainEventHandler<SetShopCustomerSubsetSettlementEvent>
    {
        public void Handle(SetShopCustomerSubsetSettlementEvent @event)
        {
            foreach (var shopCustomerSubset in @event.Shop.CustomerSubsets)
            {
                shopCustomerSubset.SetSettlement();
            }
        }
    }
}