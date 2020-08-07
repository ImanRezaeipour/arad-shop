using System;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Brands.Aggregates
{
    public class Brand : AggregateRoot<Guid>, IPassivable
    {
        protected Brand() { }
        public Brand(Guid id, string name, string latinName)
        {
            Id = id;
            Name = name;
            LatinName = latinName;
            IsActive = true;
        }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public bool IsActive { get; private set; }
        public void Active() => IsActive = true;
        public void DeActive() => IsActive = false;
        public override void Validate()
        {
        }
    }
}