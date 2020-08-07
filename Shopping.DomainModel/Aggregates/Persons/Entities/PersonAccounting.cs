using System;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Persons.Entities
{
    public class PersonAccounting : Entity, IHasCreationTime
    {
        protected PersonAccounting()
        {
        }
        public PersonAccounting(Guid id, long detailCode)
        {
            DetailCode = detailCode;
            Id = id;
            CreationTime = DateTime.Now;
        }
        public DateTime CreationTime { get; private set; }
        public long DetailCode { get; set; }
        public override void Validate()
        {
        }
    }
}