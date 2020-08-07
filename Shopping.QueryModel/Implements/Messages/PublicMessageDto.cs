using System;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels;
using Shopping.QueryModel.QueryModels.Messages;

namespace Shopping.QueryModel.Implements.Messages
{
    public class PublicMessageDto: IPublicMessageDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreationTime { get; set; }
        public PublicMessageType PublicMessageType { get; set; }
        public IUserInfoDto UserInfo { get; set; }
    }
}