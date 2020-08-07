using System;
using Shopping.DomainModel.Aggregates.ShopMarketersHistories.Aggregates;
using Shopping.DomainModel.Aggregates.ShopMarketersHistories.Events;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Infrastructure.Enum;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.ShopMarketersHistories.EventHandlers
{
    public class ShopMarketersHistoryEventHandler :
        IDomainEventHandler<AssignmentShopMarketersHistoryEvent>,
        IDomainEventHandler<DetachedShopMarketersHistoryEvent>
    {
        private readonly IRepository<ShopMarketersHistory> _repository;
        public ShopMarketersHistoryEventHandler(IRepository<ShopMarketersHistory> repository)
        {
            _repository = repository;
        }
        public void Handle(AssignmentShopMarketersHistoryEvent @event)
        {
            var shopMarketersHistory = new ShopMarketersHistory(Guid.NewGuid(), @event.Shop, @event.Marketer,ShopMarketersHistoryType.Assignment,@event.UserInfo);
            _repository.Add(shopMarketersHistory);
        }
        public void Handle(DetachedShopMarketersHistoryEvent @event)
        {
            var shopMarketersHistory = new ShopMarketersHistory(Guid.NewGuid(), @event.Shop, @event.Marketer, ShopMarketersHistoryType.Detached, @event.UserInfo);
            _repository.Add(shopMarketersHistory);
        }
    }
}