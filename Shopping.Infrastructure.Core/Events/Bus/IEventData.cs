using System;

namespace Shopping.Infrastructure.Core.Events.Bus
{
    public interface IEventData
    {
        Guid EventId { get; }
        DateTime EventTime { get; }
        object EventSource { get; set; }
    }
}