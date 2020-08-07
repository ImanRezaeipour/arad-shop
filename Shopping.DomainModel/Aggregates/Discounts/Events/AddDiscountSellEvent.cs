using System;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Discounts.Events
{
    public class AddDiscountSellEvent : IDomainEvent
    {
        public AddDiscountSellEvent(Guid discountId, decimal shopDebitPrice, decimal financialBenefit, OrderBase order, Product product, Customer customer, DiscountSellType discountSellType, Factor factor, Shop shop)
        {
            DiscountId = discountId;
            ShopDebitPrice = shopDebitPrice;
            FinancialBenefit = financialBenefit;
            Order = order;
            Product = product;
            Customer = customer;
            DiscountSellType = discountSellType;
            Factor = factor;
            Shop = shop;
            DomainEventDate = DateTime.Now;
        }
        public DateTime DomainEventDate { get; }
        public Guid DiscountId { get; private set; }
        public decimal ShopDebitPrice { get; private set; }
        public decimal FinancialBenefit { get; private set; }
        public OrderBase Order { get; private set; }
        public Product Product { get; private set; }
        public Customer Customer { get; private set; }
        public Factor Factor { get; private set; }
        public Shop Shop { get; private set; }
        public DiscountSellType DiscountSellType { get; private set; }
    }
}