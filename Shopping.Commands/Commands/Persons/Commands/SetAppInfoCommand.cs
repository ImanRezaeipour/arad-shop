using System;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Shared.Enums;

namespace Shopping.Commands.Commands.Persons.Commands
{
    public class SetAppInfoCommand : ShoppingCommandBase
    {
        public Guid UserId { get; set; }
        public string PushToken { get; set; }
        public OsType OsType { get; set; }
        public AppType AppType { get; set; }
        public string AuthDeviceId { get; set; }
    }
    public class MuteAppCommand : ShoppingCommandBase
    {
        public Guid UserId { get; set; }
        public AppType AppType { get; set; }
    }
    public class UnMuteAppCommand : ShoppingCommandBase
    {
        public Guid UserId { get; set; }
        public AppType AppType { get; set; }
    }
}