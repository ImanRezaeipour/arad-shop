using System;

namespace Shopping.Infrastructure.Core.DomainEvent
{
    public interface IDomainEvent
    {
        /// <summary>
        /// زمان ایجاد @event
        /// </summary>
        DateTime DomainEventDate { get;}
    }
}