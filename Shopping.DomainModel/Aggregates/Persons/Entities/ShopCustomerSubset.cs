using System;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Persons.Entities
{
    public class ShopCustomerSubset : Entity, IHasCreationTime
    {
        protected ShopCustomerSubset() { }
        public ShopCustomerSubset(Guid id, Customer customer)
        {
            Id = id;
            Customer = customer;
            CreationTime = DateTime.Now;
        }
        public virtual Customer Customer { get; set; }
        public DateTime CreationTime { get; private set; }
        public bool HavePaidFactor { get; private set; }
        public DateTime? PaidFactorDate { get; private set; }
        public bool IsSettlement { get; private set; }
        public DateTime? SettlementDate { get; private set; }
        public void SetHavePaidFactor()
        {
            if (HavePaidFactor) return;
            HavePaidFactor = true;
            PaidFactorDate = DateTime.Now;
        }
        public void SetSettlement()
        {
            IsSettlement = true;
            SettlementDate = DateTime.Now;
        }
        public override void Validate()
        {
        }
    }
}