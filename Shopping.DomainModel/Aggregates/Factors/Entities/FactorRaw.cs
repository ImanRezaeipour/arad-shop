using System;
using Shopping.DomainModel.Aggregates.Factors.Entities.Discounts.Abstract;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.Factors.Entities
{
    public  class FactorRaw : Entity<long>
    {
        protected FactorRaw() { }
        public FactorRaw(Guid productId, int quantity, string description, decimal price, string productName,
            string productImage, Guid brandId, string brandName, FactorRawDiscountBase discount)
        {
            ProductId = productId;
            Quantity = quantity;
            Description = description;
            Price = price;
            ProductName = productName;
            ProductImage = productImage;
            BrandId = brandId;
            BrandName = brandName;
            Discount = discount;
        }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public string ProductName { get; private set; }
        public string ProductImage { get; private set; }
        public Guid BrandId { get; private set; }
        public string BrandName { get; private set; }
        public virtual FactorRawDiscountBase Discount { get; set; }
        public override void Validate()
        {
        }
    }
}