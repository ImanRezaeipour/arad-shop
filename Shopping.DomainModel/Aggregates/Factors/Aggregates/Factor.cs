using System;
using System.Collections.Generic;
using System.Linq;
using Parbad.Core;
using Shopping.DomainModel.Aggregates.Factors.Entities;
using Shopping.DomainModel.Aggregates.Factors.Entities.Accounting;
using Shopping.DomainModel.Aggregates.Factors.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Factors.Entities.ShipmentState;
using Shopping.DomainModel.Aggregates.Factors.Entities.State;
using Shopping.DomainModel.Aggregates.Factors.ValueObjects;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Factors.Aggregates
{
    public class Factor : AggregateRoot<long>, IHasCreationTime
    {
        protected Factor() { }
        public Factor(long id, long orderId, decimal price, Guid orderSuggestionId,
            int discount, Customer customer, Shop shop, FactorAddress factorAddress,
            decimal discountPrice, decimal systemDiscountPrice, int shippingTime)
        {
            SystemDiscountPrice = systemDiscountPrice;
            Id = id;
            Shop = shop;
            Price = price;
            OrderId = orderId;
            Discount = discount;
            Customer = customer;
            CreationTime = DateTime.Now;
            FactorStateBase = new PendingFactorState(Guid.NewGuid());
            FactorState = FactorState.Pending;
            ShipmentState = ShipmentState.Pending;
            ShipmentStateBase = new PendingShipmentState(Guid.NewGuid());
            OrderSuggestionId = orderSuggestionId;
            FactorAddress = factorAddress;
            DiscountPrice = discountPrice;
            ShippingTime = shippingTime;
        }
        public virtual Customer Customer { get; private set; }
        public virtual Shop Shop { get; private set; }
        public DateTime CreationTime { get; private set; }
        public long OrderId { get; private set; }
        public Guid OrderSuggestionId { get; private set; }
        public decimal Price { get; private set; }
        public int Discount { get; private set; }
        /// <summary>
        /// shop discount
        /// </summary>
        public decimal DiscountPrice { get; private set; }
        /// <summary>
        /// dicount system
        /// </summary>
        public decimal SystemDiscountPrice { get; private set; }
        public int ShippingTime { get; private set; }
        public virtual FactorStateBase FactorStateBase { get; private set; }
        public FactorState FactorState { get; set; }
        public virtual ShipmentStateBase ShipmentStateBase { get; set; }
        public ShipmentState ShipmentState { get; private set; }
        public virtual List<FactorRaw> FactorRaws { get; set; }
        public virtual FactorAddress FactorAddress { get; private set; }
        public FactorAccounting Accounting { get; private set; }
        public void RegisterInAccountingSystem(string accountingId)
        {
            Accounting = new FactorAccounting(Guid.NewGuid(), accountingId);
        }
        public void RegisterRecPayInAccountingSystem()
        {
            Accounting = new FactorAccounting(Guid.NewGuid(), Accounting.GRDBId)
            {
                RecPay = true,
                RecPayCreationTime = DateTime.Now
            };
        }
        public void PayFactor(Guid id, string referenceId, string transactionId, VerifyResultStatus status, string message)
        {
            FactorState = FactorState.Paid;
            FactorStateBase = new PaidFactorState(id, referenceId, transactionId, status, message);
        }
        public void FailedFactor(Guid id, string referenceId, string transactionId, VerifyResultStatus status, string message)
        {
            FactorState = FactorState.Failed;
            FactorStateBase = new FailedFactorState(id, referenceId, transactionId, status, message);
        }
        public bool HaveFactorRawPercentDiscount()
        {
            return FactorRaws.Any(p => p.Discount != null && p.Discount is FactorRawPercentDiscount);
        }
        public void SendShipment()
        {
            ShipmentStateBase = new SendShipmentState(Guid.NewGuid());
            ShipmentState = ShipmentState.Send;
        }
        public void DeliveryShipment()
        {
            ShipmentStateBase = new DeliveryShipmentState(Guid.NewGuid());
            ShipmentState = ShipmentState.Delivery;
        }
        public void ReverseShipment(string reason)
        {
            ShipmentStateBase = new ReverseShipmentState(Guid.NewGuid(), reason);
            ShipmentState = ShipmentState.Reverse;
        }
        public override void Validate()
        {
        }
    }
}