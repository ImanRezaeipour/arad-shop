using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.ShopOrderLogs.Aggregates;
using Shopping.DomainModel.Aggregates.ShopOrderLogs.Events;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.ShopOrderLogs.EventHandlers
{
    public class ShopOrderLogEventHandler : IDomainEventHandler<CreateHasFactorEvent>
        , IDomainEventHandler<CreateHasSuggestionsEvent>
        , IDomainEventHandler<CreateShopOrderLogEvent>
    {
        private readonly IRepository<ShopOrderLog> _repository;
        public ShopOrderLogEventHandler(IRepository<ShopOrderLog> repository)
        {
            _repository = repository;
        }
        public void Handle(CreateHasFactorEvent @event)
        {
            var shopOrderLog = _repository.AsQuery()
                .SingleOrDefault(p => p.Shop.Id == @event.ShopId && p.Order.Id == @event.OrderId);
            if (shopOrderLog == null) return;
            shopOrderLog.DateHasFactor = @event.DomainEventDate;
            shopOrderLog.HasFactor = true;
            shopOrderLog.Factor = @event.Factor;
        }
        public void Handle(CreateHasSuggestionsEvent @event)
        {
            var shopOrderLog = _repository.AsQuery()
                .SingleOrDefault(p => p.Shop.Id == @event.ShopId && p.Order.Id == @event.OrderId);
            if (shopOrderLog == null) return;
            shopOrderLog.DateHasSuggestions = @event.DomainEventDate;
            shopOrderLog.HasSuggestions = true;
            shopOrderLog.OrderSuggestion = @event.OrderSuggestion;
        }
        public void Handle(CreateShopOrderLogEvent @event)
        {
            var shopOrderLog = new ShopOrderLog(Guid.NewGuid(), @event.Shop, @event.Order);
            _repository.Add(shopOrderLog);
        }
    }
}