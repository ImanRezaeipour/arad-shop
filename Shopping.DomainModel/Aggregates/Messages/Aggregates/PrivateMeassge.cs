using System;
using Shopping.DomainModel.Aggregates.Messages.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Shared;

namespace Shopping.DomainModel.Aggregates.Messages.Aggregates
{
    public class PrivateMeassge:Message
    {
        protected PrivateMeassge(){}
        public PrivateMeassge(Guid id, string title, string body, UserInfo userInfo, Guid personId) : base(id, title, body, userInfo)
        {
            PersonId = personId;
        }
        public Guid PersonId { get; set; }
        public override void Validate()
        {
        }
    }
}