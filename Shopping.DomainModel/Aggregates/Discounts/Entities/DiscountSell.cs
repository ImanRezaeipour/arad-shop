using System;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Discounts.Entities
{
    public class DiscountSell : Entity, IHasCreationTime
    {
        protected DiscountSell()
        {
        }
        public DiscountSell(Guid id, Product product,
            Customer customer, DiscountSellType discountSellType,
            decimal shopDebitPrice, decimal financialBenefit, OrderBase order, Shop shop, Factor factor)
        {
            Id = id;
            Product = product;
            Customer = customer;
            DiscountSellType = discountSellType;
            ShopDebitPrice = shopDebitPrice;
            FinancialBenefit = financialBenefit;
            Order = order;
            Shop = shop;
            Factor = factor;
            CreationTime = DateTime.Now;
        }
        public decimal ShopDebitPrice { get; private set; }
        public decimal FinancialBenefit { get; private set; }
        public virtual Shop Shop { get; private set; }
        public virtual OrderBase Order { get; private set; }
        public virtual Product Product { get; private set; }
        public virtual Customer Customer { get; private set; }
        public virtual Factor Factor { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DiscountSellType DiscountSellType { get; private set; }
        public override void Validate()
        {
        }

    }
}