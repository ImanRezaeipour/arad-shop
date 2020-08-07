using System;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Orders.ValueObjects;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Orders.Aggregates
{
    public class AreaOrder : OrderBase
    {
        protected AreaOrder()
        {
        }
        public AreaOrder(Customer customer, OrderAddress orderAddress, string description, DateTime expireOpenTime, Shop shop,
            PrivateOrder privateOrder, AreaOrderCreator creator) : base(customer, orderAddress, description, expireOpenTime)
        {
            Shop = shop;
            PrivateOrder = privateOrder;
            Creator = creator;
        }
        public virtual Shop Shop { get; private set; }
        public virtual PrivateOrder PrivateOrder { get; private set; }
        public AreaOrderCreator Creator { get;private set; }
        public override void Validate()
        {
        }
    }
}