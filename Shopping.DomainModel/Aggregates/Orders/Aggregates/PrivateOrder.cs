using System;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Orders.ValueObjects;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Orders.Aggregates
{
    public class PrivateOrder : OrderBase
    {
        protected PrivateOrder()
        {
        }
        public PrivateOrder(Customer customer, OrderAddress orderAddress,
            string description, DateTime expireOpenTime, Shop shop)
            : base(customer, orderAddress, description, expireOpenTime)
        {
            Shop = shop;
            IsConvertToAreaOrder = false;
        }
        public virtual Shop Shop { get; set; }
        public bool IsConvertToAreaOrder { get; set; }
        public override void Validate()
        {
        }
    }
}