using System;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.Marketers.Entities
{
    public class MarketerComment : Entity<Guid>
    {
        protected MarketerComment(){}
        public MarketerComment(Guid id,string comment)
        {
            Id = id;
            Comment = comment;
        }
        public string Comment { get;private set; }
        public override void Validate()
        {
        }
    }
}