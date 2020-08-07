using System;
using System.Collections.Generic;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Promoters.Entities;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Promoters.Aggregates
{
    public class Promoter : AggregateRoot, IHasCreationTime
    {
        protected Promoter() { }
        public Promoter(Guid id, long code, string firstName, string lastName, string nationalCode, string mobileNumber)
        {
            Id = id;
            Code = code;
            LastName = lastName;
            FirstName = firstName;
            CreationTime = DateTime.Now;
            NationalCode = nationalCode;
            MobileNumber = mobileNumber;
            CustomerSubsetCount = 0;
        }
        public long Code { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public DateTime CreationTime { get; private set; }
        public int CustomerSubsetCount { get; private set; }
        public virtual ICollection<PromoterCustomerSubset> CustomerSubsets { get; set; }
        public void AddCustomerSubset(Customer customer)
        {
            CustomerSubsets.Add(new PromoterCustomerSubset(Guid.NewGuid(), customer));
            CustomerSubsetCount++;
        }
        public override void Validate()
        {
        }
    }
}