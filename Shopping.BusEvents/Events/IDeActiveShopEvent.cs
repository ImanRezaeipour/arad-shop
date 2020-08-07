using System;

namespace Shopping.BusEvents.Events
{
    public interface IDeActiveShopEvent
    {
        Guid UserId { get; }
    }
}