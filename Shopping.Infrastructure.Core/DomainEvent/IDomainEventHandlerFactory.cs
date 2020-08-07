using System.Collections.Generic;

namespace Shopping.Infrastructure.Core.DomainEvent
{
    public interface IDomainEventHandlerFactory
    {
        IEnumerable<IDomainEventHandler<T>> GetDomainEventHandlersFor<T>(T domainEvent) where T : IDomainEvent;
    }
}