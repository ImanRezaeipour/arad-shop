using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Messages.Commands
{
    public class CreatePrivateMessageCommand:ShoppingCommandBase
    {
        public Guid PersonId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}