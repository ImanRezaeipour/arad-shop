using Shopping.DomainModel.Aggregates.Orders.Events;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Orders.EventHandlers
{
    public class OrderEventHandler : IDomainEventHandler<TheOrderStatusWentToHasSuggestionEvent>
        ,IDomainEventHandler<TheOrderStatusWentToAcceptEvent>
    {
        public void Handle(TheOrderStatusWentToHasSuggestionEvent @event)
        {
            @event.Order.OrderStatus = OrderStatus.HasSuggestion;
        }

        public void Handle(TheOrderStatusWentToAcceptEvent @event)
        {
            @event.OrderBase.OrderStatus = OrderStatus.Accept;
        }
    }
}