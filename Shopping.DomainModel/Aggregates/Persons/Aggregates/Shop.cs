using System;
using System.Collections.Generic;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Entities;
using Shopping.DomainModel.Aggregates.Persons.ValueObjects;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Persons.Aggregates
{
    public class Shop : Person, IHasCreationTime
    {
        protected Shop()
        {
        }
        public Shop(Guid id, string name, string firstName, string lastName, string emailAddress,
            Guid userId, string description, string nationalCode, ShopAddress address, BankAccount bankAccount,
            ImageDocuments imageDocuments, string mobileNumber, int areaRadius, int metrage,
            int defaultDiscount, long marketerId, long personNumber)
            : base(id, firstName, lastName, emailAddress, userId, mobileNumber, personNumber)
        {
            Name = name;
            ShopStatus = ShopStatus.Pending;
            Description = description;
            NationalCode = nationalCode;
            ShopAddress = address;
            BankAccount = bankAccount;
            CreationTime = DateTime.Now;
            ImageDocuments = imageDocuments;
            AreaRadius = areaRadius;
            Metrage = metrage;
            DefaultDiscount = defaultDiscount;
            MarketerId = marketerId;
            RecommendCode = personNumber;
        }
        public string Name { get; set; }
        public ShopStatus ShopStatus { get; set; }
        public string DescriptionStatus { get; set; }
        public string Description { get; set; }
        public string NationalCode { get; set; }
        public int AreaRadius { get; set; }
        public int Metrage { get; set; }
        public int DefaultDiscount { get; set; }
        public long? MarketerId { get; set; }
        public long RecommendCode { get; private set; }
        public DateTime CreationTime { get; private set; }
        public virtual ShopAddress ShopAddress { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual ImageDocuments ImageDocuments { get; set; }
        public virtual ICollection<ShopStatusLog> ShopStatusLogs { get; set; }
        public virtual ICollection<ShopCustomerSubset> CustomerSubsets { get; set; }
        public void AddCustomerSubset(Customer customer)
        {
            CustomerSubsets.Add(new ShopCustomerSubset(Guid.NewGuid(), customer));
        }
        public override void Validate()
        {
        }
    }
}