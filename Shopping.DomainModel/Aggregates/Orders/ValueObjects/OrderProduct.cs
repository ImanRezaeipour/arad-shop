using System;
using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Orders.ValueObjects
{
    public class OrderProduct : ValueObject<OrderProduct>
    {
        protected OrderProduct() { }
        public OrderProduct(Guid productId, string name, decimal price, string productImage, Guid brandId, string brandName)
        {
            Price = price;
            ProductId = productId;
            Name = name;
            ProductImage = productImage;
            BrandId = brandId;
            BrandName = brandName;
        }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string ProductImage { get; private set; }
        public Guid BrandId { get; private set; }
        public string BrandName { get; private set; }
    }
}