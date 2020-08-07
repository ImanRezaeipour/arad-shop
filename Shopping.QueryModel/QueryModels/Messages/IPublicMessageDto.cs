using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.Messages
{
    public interface IPublicMessageDto : IMessageDto
    {
        PublicMessageType PublicMessageType { get; set; }
    }
}