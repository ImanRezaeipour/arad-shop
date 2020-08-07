using System;
using Shopping.Shared.Enums;
using Shopping.Shared.Events.Interfaces.Users;

namespace Shopping.Shared.Events.Messages.Users
{
    public class DeActiveUserEvent : IDeActiveUserEvent
    {
        public DeActiveUserEvent(Guid userId, AppType appType)
        {
            UserId = userId;
            AppType = appType;
        }
        public Guid UserId { get; private set; }
        public AppType AppType { get; private set; }
    }
}