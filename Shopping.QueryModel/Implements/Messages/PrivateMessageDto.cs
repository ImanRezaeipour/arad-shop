using System;
using Shopping.QueryModel.QueryModels;
using Shopping.QueryModel.QueryModels.Messages;

namespace Shopping.QueryModel.Implements.Messages
{
    public class PrivateMessageDto : IPrivateMessageDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid PersonId { get; set; }
        public DateTime CreationTime { get; set; }
        public IUserInfoDto UserInfo { get; set; }
    }
}