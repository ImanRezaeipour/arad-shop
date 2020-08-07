using Shopping.DomainModel.Aggregates.Notifications.Aggregates;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Notifications.Events;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Repository.Write.Interface;
using Shopping.SignalR.Hubs;
using Shopping.SignalR.Hubs.Messages;

namespace Shopping.DomainModel.Aggregates.Notifications.EventHandlers
{
    public class NotificationEventHandler : IDomainEventHandler<AddPanelNotificationEvent>
    {
        private readonly IRepository<NotificationBase> _repository;
        public NotificationEventHandler(IRepository<NotificationBase> repository)
        {
            _repository = repository;
        }
        public void Handle(AddPanelNotificationEvent @event)
        {
            var panelNotification =
                new PanelNotification(@event.Id, @event.Title, @event.Body, @event.PanelNotificationType,@event.AdditionalData);
            _repository.Add(panelNotification);
            SignalRSender.NotifyShopCreated(new PanelNotificationMessage(@event.Id, @event.Title, @event.Body, @event.PanelNotificationType,@event.AdditionalData));
        }
    }
}