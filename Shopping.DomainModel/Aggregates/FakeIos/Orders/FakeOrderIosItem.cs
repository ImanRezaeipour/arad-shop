using System;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.FakeIos.Orders
{
    public class FakeOrderIosItem:Entity<Guid>
    {
        protected FakeOrderIosItem()
        {
        }
        public FakeOrderIosItem(Guid id, Guid productId, string name, string image, string brand, int quantity)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Image = image;
            Brand = brand;
            Quantity = quantity;
        }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public override void Validate()
        {
        }
    }
}