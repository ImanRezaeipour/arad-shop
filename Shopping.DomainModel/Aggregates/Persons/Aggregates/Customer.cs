using System;
using System.Collections.Generic;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Entities.CustomerEntities;
using Shopping.DomainModel.Aggregates.Persons.ValueObjects;

namespace Shopping.DomainModel.Aggregates.Persons.Aggregates
{
    public class Customer : Person
    {
        protected Customer()
        {
        }
        public Customer(Guid id, string name, string lastName, string emailAddress, Guid userId, DefultCustomerAddress defultCustomerAddress, string mobileNumber, long personNumber, DateTime birthDate)
            : base(id, name, lastName, emailAddress, userId, mobileNumber, personNumber)
        {
            DefultCustomerAddress = defultCustomerAddress;
            BirthDate = birthDate;
        }
        public DateTime BirthDate { get; set; }
        public virtual DefultCustomerAddress DefultCustomerAddress { get;  set; }
        public virtual CustomerRecommender Recommender { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public override void Validate()
        {
        }
    }
}