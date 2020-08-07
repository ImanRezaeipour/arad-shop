using System;
using Shopping.DomainModel.Aggregates.Messages.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Messages.Aggregates
{
    public class PublicMessage : Message
    {
        protected PublicMessage() { }
        public PublicMessage(Guid id, string title, string body, UserInfo userInfo, PublicMessageType publicMessageType) : base(id, title, body, userInfo)
        {
            PublicMessageType = publicMessageType;
        }
        public PublicMessageType PublicMessageType { get; set; }
        public override void Validate()
        {
        }
    }
}