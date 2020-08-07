using System;
using PersianDate;
using Shopping.Infrastructure.Enum;

namespace Shopping.SignalR.Hubs.Messages
{
    public class PanelNotificationMessage
    {
        public PanelNotificationMessage(Guid id,
             string title, string body, PanelNotificationType type, string additionalData)
        {
            Id = id;
            Type = type;
            AdditionalData = additionalData;
            CreationTime = DateTime.Now.ToFa("G");
            Title = title;
            Body = body;
        }
        public Guid Id { get; }
        public PanelNotificationType Type { get; }
        public string CreationTime { get; }
        public string Title { get; }
        public string Body { get; }
        public string AdditionalData { get; }
    }
}