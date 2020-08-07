using System.Collections.Generic;

namespace Shopping.Infrastructure.Core.DomainEvent
{
    public class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        public IEnumerable<IDomainEventHandler<T>> GetDomainEventHandlersFor<T>(T domainEvent) where T : IDomainEvent
        {
            return DomainEventServiceLocator.GetAllInstances<IDomainEventHandler<T>>();
        }
    }
}