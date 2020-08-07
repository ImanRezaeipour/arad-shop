
namespace Shopping.Infrastructure.Core.DomainEvent
{
    public class DomainEventDispatcher
    {
        private static readonly IDomainEventHandlerFactory DomainEventHandlerFactory;
        static DomainEventDispatcher()
        {
            DomainEventHandlerFactory = DomainEventServiceLocator.GetInstance<IDomainEventHandlerFactory>();
        }
        public static void Raise<T>(T @event) where T : IDomainEvent
        {
            foreach (var domainEventHandler in DomainEventHandlerFactory.GetDomainEventHandlersFor(@event))
            {
                domainEventHandler.Handle(@event);
            }
        }
    }
}