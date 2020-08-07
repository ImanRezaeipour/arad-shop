using System;
using System.Collections.Generic;
using Shopping.DomainModel.Aggregates.Factors.Entities;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.Factors.Events
{
    public class CreateFactorEvent : IDomainEvent
    {
        public CreateFactorEvent(long factorId, long orderId, Guid orderSuggestionId, decimal price, int discount, List<FactorRaw> factorRaws, Shop shop, Customer customer, OrderBase orderBase, int shippingTime)
        {
            FactorId = factorId;
            OrderId = orderId;
            OrderSuggestionId = orderSuggestionId;
            Price = price;
            Discount = discount;
            FactorRaws = factorRaws;
            Shop = shop;
            Customer = customer;
            OrderBase = orderBase;
            ShippingTime = shippingTime;
            DomainEventDate = DateTime.Now;
        }
        public long FactorId { get; private set; }
        public long OrderId { get; private set; }
        public int Discount { get; private set; }
        public decimal Price { get; private set; }
        public Guid OrderSuggestionId { get; private set; }
        public DateTime DomainEventDate { get; private set; }
        public Shop Shop { get; private set; }
        public Customer Customer { get; private set; }
        public List<FactorRaw> FactorRaws { get; private set; }
        public OrderBase OrderBase { get; private set; }
        public int ShippingTime { get; private set; }
    }
}
