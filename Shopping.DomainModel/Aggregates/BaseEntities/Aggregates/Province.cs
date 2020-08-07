using System;
using System.Collections.Generic;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.BaseEntities.Aggregates
{
    public class Province : AggregateRoot<Guid>
    {
        protected Province()
        {
        }
        public Province(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public override void Validate()
        {
        }
    }
}