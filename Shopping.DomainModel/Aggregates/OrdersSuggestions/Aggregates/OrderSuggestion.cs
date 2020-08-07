using System;
using System.Collections.Generic;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.OrdersSuggestions.Aggregates
{
    public class OrderSuggestion : AggregateRoot, IHasCreationTime
    {
        protected OrderSuggestion() { }
        public OrderSuggestion(Guid id, long orderId, int discount, string description, Shop shop, int shippingTime)
        {
            Id = id;
            Shop = shop;
            OrderId = orderId;
            Discount = discount;
            Description = description;
            CreationTime = DateTime.Now;
            OrderSuggestionStatus = OrderSuggestionStatus.Pending;
            ShippingTime = shippingTime;
        }
        public long OrderId { get; private set; }
        public int Discount { get; private set; }
        public string Description { get; private set; }
        public virtual Shop Shop { get; private set; }
        public DateTime CreationTime { get; private set; }
        public int ShippingTime { get; private set; }
        public OrderSuggestionStatus OrderSuggestionStatus { get; set; }
        public virtual ICollection<OrderSuggestionItemBase> OrderSuggestionItems { get; set; }
        public override void Validate()
        {
        }
    }
}