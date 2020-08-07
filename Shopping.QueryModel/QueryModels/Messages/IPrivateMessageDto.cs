using System;

namespace Shopping.QueryModel.QueryModels.Messages
{
    public interface IPrivateMessageDto : IMessageDto
    {
        Guid PersonId { get; set; }
    }
}