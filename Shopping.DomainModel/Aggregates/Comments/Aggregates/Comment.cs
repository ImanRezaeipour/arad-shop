using System;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;


namespace Shopping.DomainModel.Aggregates.Comments.Aggregates
{
    public class Comment : AggregateRoot, IHasCreationTime
    {
        protected Comment()
        { }
        public Comment(Guid id, int degree, int itemDegree, long factorId)
        {
            Id = id;
            Degree = degree;
            ItemDegree = itemDegree;
            FactorId = factorId;
            CreationTime = DateTime.Now;
        }
        public int Degree { get; set; }
        public int ItemDegree { get; set; }
        public long FactorId { get; private set; }
        public DateTime CreationTime { get; set; }
        public override void Validate()
        {
        }
    }
}