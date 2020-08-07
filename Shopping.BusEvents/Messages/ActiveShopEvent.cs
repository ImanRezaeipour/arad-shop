using System;
using Shopping.BusEvents.Events;

namespace Shopping.BusEvents.Messages
{
    public class ActiveShopEvent: IActiveShopEvent
    {
        public ActiveShopEvent(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; private set; }
    }
}