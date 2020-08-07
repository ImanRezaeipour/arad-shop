using Shopping.Commands.Commands.Shared;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Messages.Commands
{
    public class CreatePublicMessageCommand : ShoppingCommandBase
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public PublicMessageType MessageType { get; set; }
        public UserInfoCommandItem UserInfo { get; set; }
    }
}