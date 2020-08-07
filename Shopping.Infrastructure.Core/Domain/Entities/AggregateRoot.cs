using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Core.Events.Bus;

namespace Shopping.Infrastructure.Core.Domain.Entities
{
    public abstract class AggregateRoot : AggregateRoot<Guid>
    {
    }
    public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {
        [NotMapped]
        public virtual ICollection<IEventData> DomainEvents { get; }
        protected AggregateRoot()
        {
            DomainEvents = new Collection<IEventData>();
        }
        protected void Publish(IEventData @event)
        {
            DomainEvents.Add(@event);
        }
        public void ClearEvents()
        {
            DomainEvents.Clear();
        }
        public IReadOnlyList<IEventData> GetUnCommitedEvents()
        {
            return DomainEvents.ToList().AsReadOnly();
        }
    }
}