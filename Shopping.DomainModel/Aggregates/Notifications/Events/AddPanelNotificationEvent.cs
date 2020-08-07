using System;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Notifications.Events
{
    public class AddPanelNotificationEvent:DomainEvent
    {
        public AddPanelNotificationEvent (Guid id, string title, string body, PanelNotificationType panelNotificationType,string additionalData)
        {
            Id = id;
            Title = title;
            Body = body;
            PanelNotificationType = panelNotificationType;
            AdditionalData = additionalData;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public PanelNotificationType PanelNotificationType { get; set; }
        public string AdditionalData { get; set; }
    }
}