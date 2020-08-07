using System;
using Shopping.BusEvents.Events;

namespace Shopping.BusEvents.Messages
{
    public class DeActiveShopEvent : IDeActiveShopEvent
    {
        public DeActiveShopEvent(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; private set; }
    }
}