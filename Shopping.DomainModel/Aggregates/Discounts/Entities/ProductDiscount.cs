using System;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Discounts.Entities
{
    public class ProductDiscount : Entity, IHasCreationTime
    {
        protected ProductDiscount() { }
        public ProductDiscount(Guid id, Product product, UserInfo userInfo)
        {
            Id = id;
            Product = product;
            UserInfo = userInfo;
            CreationTime = DateTime.Now;
        }
        public virtual Product Product { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public override void Validate()
        {
        }
    }
}