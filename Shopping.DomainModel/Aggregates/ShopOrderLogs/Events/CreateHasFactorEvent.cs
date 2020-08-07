using System;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.ShopOrderLogs.Events
{
    public class CreateHasFactorEvent : IDomainEvent
    {
        public CreateHasFactorEvent(long orderId, Guid shopId, Factor factor)
        {
            DomainEventDate = DateTime.Now;
            OrderId = orderId;
            ShopId = shopId;
            Factor = factor;
        }
        public long OrderId { get; set; }
        public Guid ShopId { get; set; }
        public DateTime DomainEventDate { get; set; }
        public Factor Factor { get; set; }
    }
}