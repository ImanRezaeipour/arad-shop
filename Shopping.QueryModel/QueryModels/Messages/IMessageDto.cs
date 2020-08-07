using System;

namespace Shopping.QueryModel.QueryModels.Messages
{
    public interface IMessageDto
    {
        string Title { get; set; }
        string Body { get; set; }
        DateTime CreationTime { get; set; }
    }
}