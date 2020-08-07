using System;
using Shopping.Shared.Enums;

namespace Shopping.Shared.Events.Interfaces.Users
{
    public interface IActiveUserEvent
    {
        Guid UserId { get; }
        AppType AppType { get; }
    }
}