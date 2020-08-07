using System;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.DomainModel.Aggregates.ShopAcceptors.ValueObjects;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.ShopAcceptors.Aggregates
{
    public class ShopAcceptor : AggregateRoot, IHasCreationTime
    {
        protected ShopAcceptor()
        { }

        public ShopAcceptor(Guid id, string firstName, string lastName, string phoneNumber, string mobileNumber, string shopName, UserInfo userInfo, ShopAcceptorAddress shopAcceptorAddress)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            MobileNumber = mobileNumber;
            ShopName = shopName;
            CreationTime = DateTime.Now;
            UserInfo = userInfo;
            ShopAcceptorAddress = shopAcceptorAddress;
            ShopAcceptorStatus = ShopAcceptorStatus.Pending;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string ShopName { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ShopAcceptorAddress ShopAcceptorAddress { get; set; }
        public ShopAcceptorStatus ShopAcceptorStatus { get; set; }
        public override void Validate()
        {
        }
    }
}