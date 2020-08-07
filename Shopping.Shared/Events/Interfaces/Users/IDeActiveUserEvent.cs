using System;
using Shopping.Shared.Enums;

namespace Shopping.Shared.Events.Interfaces.Users
{
    public interface IDeActiveUserEvent
    {
        Guid UserId { get; }
        AppType AppType { get; }
    }
}