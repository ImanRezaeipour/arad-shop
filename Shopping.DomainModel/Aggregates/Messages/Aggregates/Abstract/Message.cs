using System;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Messages.Aggregates.Abstract
{
    public abstract class Message : AggregateRoot, IHasCreationTime
    {
        protected Message()
        {
        }
        protected Message(Guid id, string title, string body, UserInfo userInfo)
        {
            Id = id;
            Title = title;
            Body = body;
            UserInfo = userInfo;
            CreationTime = DateTime.Now;
        }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public DateTime CreationTime { get; private set; }
        public virtual UserInfo UserInfo { get;private set; }
    }
}