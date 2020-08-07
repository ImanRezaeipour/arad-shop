using System;
using Shopping.DomainModel.Aggregates.Orders.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Orders.ValueObjects;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.Orders.Entities
{
    public class OrderItem : Entity
    {
        protected OrderItem() { }
        public OrderItem(Guid id, int quantity, string description, OrderProduct orderProduct, OrderItemDiscountBase discount)
        {
            Id = id;
            Quantity = quantity;
            Description = description;
            OrderProduct = orderProduct;
            Discount = discount;
        }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public virtual OrderProduct OrderProduct { get; set; }
        public virtual OrderItemDiscountBase Discount { get; set; }
        public override void Validate()
        {
        }
    }
}