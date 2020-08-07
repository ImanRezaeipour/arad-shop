using System.Collections.Generic;
using Shopping.Infrastructure.Core.Events.Bus;

namespace Shopping.Infrastructure.Core.Domain.Entities.Interfaces
{
    public interface IAggregateRoot
    {
    }
    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>, IGeneratesDomainEvents, IAggregateRoot
    {
        void ClearEvents();
        IReadOnlyList<IEventData> GetUnCommitedEvents();
    }
    public interface IGeneratesDomainEvents
    {
        ICollection<IEventData> DomainEvents { get; }
    }
}