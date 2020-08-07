using System;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Promoters.Entities
{
    public class PromoterCustomerSubset : Entity, IHasCreationTime
    {
        protected PromoterCustomerSubset() { }
        public PromoterCustomerSubset(Guid id, Customer customer)
        {
            Id = id;
            Customer = customer;
            CreationTime = DateTime.Now;
        }
        public DateTime CreationTime { get; private set; }
        public virtual Customer Customer { get; private set; }
        public bool HavePaidFactor { get; private set; }
        public DateTime? PaidFactorDate { get; private set; }
        public void SetHavePaidFactor()
        {
            if (HavePaidFactor) return;
            PaidFactorDate = DateTime.Now;
            HavePaidFactor = true;
        }
        public override void Validate()
        {
        }
    }
}