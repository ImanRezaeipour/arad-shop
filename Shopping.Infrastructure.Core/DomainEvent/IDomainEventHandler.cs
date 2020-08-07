namespace Shopping.Infrastructure.Core.DomainEvent
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        void Handle(T @event);
    }
}