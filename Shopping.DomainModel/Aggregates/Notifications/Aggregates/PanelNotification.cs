using System;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates.Abstract;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Notifications.Aggregates
{
    public class PanelNotification : NotificationBase
    {
        private PanelNotification()
        {
        }
        public PanelNotification(Guid id, string title, string body, PanelNotificationType type, string additionalData)
            : base(id, title, body)
        {
            Type = type;
            AdditionalData = additionalData;
            IsVisited = false;
        }
        public PanelNotificationType Type { get; private set; }
        public bool IsVisited { get; private set; }
        public string AdditionalData { get;private set; }
        public void Visited() => IsVisited = true;
        public override void Validate()
        {
        }
    }
}