using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shopping.DomainModel.Aggregates.Orders.Entities;
using Shopping.DomainModel.Aggregates.Orders.ValueObjects;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract
{
    public abstract class OrderBase : AggregateRoot<long>, IHasCreationTime
    {
        protected OrderBase() { }
        protected OrderBase(Customer customer, OrderAddress orderAddress, string description, DateTime expireOpenTime)
        {
            Customer = customer;
            ExpireOpenTime = expireOpenTime;
            OrderAddress = orderAddress;
            Description = description;
            CreationTime = DateTime.Now;
            OrderStatus = OrderStatus.Pending;
        }
        public string Description { get; private set; }
        public DateTime CreationTime { get; private set; }
        /// <summary>
        /// x time
        /// </summary>
        public DateTime ExpireOpenTime { get; set; }
        /// <summary>
        /// z time
        /// </summary>
        public DateTime? ResponseExpireTime { get; set; }
        public DateTime? SuggestionTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public virtual Customer Customer { get; private set; }
        public virtual OrderAddress OrderAddress { get; private set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public void Opened(int orderItemResponseTime)
        {
            if (OrderStatus != OrderStatus.Pending)
            {
                throw new DomainException("وضعیت جاری سفارش نامعتبر می باشد");
            }
            if (ExpireOpenTime <= DateTime.Now)
            {
                throw new DomainException("مدت زمان بازکردن سفارش توسط شما به پایان رسیده است");
            }
            OrderStatus = OrderStatus.SelfOpened;
            ResponseExpireTime = DateTime.Now.AddSeconds(OrderItems.Count * orderItemResponseTime);
        }
    }
}