using System;

namespace Shopping.Infrastructure.Core.DomainEvent
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
            DomainEventDate = DateTime.Now;
        }
        /// <summary>
        /// زمان ایجاد @event
        /// </summary>
        public DateTime DomainEventDate { get; private set; }
    }
}