using System;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Commands
{
    public class SetAppInfoCommand : ShoppingCommandBase
    {
        public Guid UserId { get; set; }
        public string PushToken { get; set; }
        public OsType OsType { get; set; }
        public AppType AppType { get; set; }
    }
}