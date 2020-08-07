using System;

namespace Shopping.BusEvents.Events
{
    public interface IActiveShopEvent
    {
        Guid UserId { get; }
    }
}