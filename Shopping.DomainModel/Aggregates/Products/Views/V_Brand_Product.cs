using Shopping.DomainModel.Aggregates.Brands.Aggregates;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Products.Entities;
using Shopping.DomainModel.Aggregates.Products.Entities.ProductDiscount;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Shopping.DomainModel.Aggregates.Products.Views
{
    public class V_Brand_Product : AggregateRoot, IPassivable, IHasCreationTime
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; private set; }
        public virtual Brand Brand { get; set; }
        public virtual CategoryBase Category { get; set; }
        public DateTime CreationTime { get; private set; }
        public string MainImage { get; set; }
       public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ProductDiscountBase ProductDiscount { get; set; }

        public override void Validate()
        {
            
        }
    }
}