using System;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Notifications.Aggregates.Abstract
{
    public abstract class NotificationBase : AggregateRoot, IHasCreationTime
    {
        protected NotificationBase()
        {
        }
        protected NotificationBase(Guid id, string title, string body)
        {
            Id = id;
            Title = title;
            Body = body;
            CreationTime = DateTime.Now;
        }
        public DateTime CreationTime { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
    }
}