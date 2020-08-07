using System;
using System.Collections.Generic;
using Shopping.DomainModel.Aggregates.Brands.Aggregates;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Products.Entities;
using Shopping.DomainModel.Aggregates.Products.Entities.ProductDiscount;
using Shopping.DomainModel.Aggregates.Products.ValueObjects;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Products.Aggregates
{
    public class Product : AggregateRoot, IPassivable, IHasCreationTime
    {
        protected Product() { }
        public Product(Guid id, string name, string shortDescription, string description, decimal price, Brand brand, CategoryBase category, string mainImage, FakeProductDiscount fakeProductDiscount)
        {
            Id = id;
            Name = name;
            MainImage = mainImage;
            ShortDescription = shortDescription;
            Description = description;
            Price = price;
            IsActive = true;
            Brand = brand;
            Category = category;
            CreationTime = DateTime.Now;
            Discount= fakeProductDiscount;
        }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; private set; }
        public virtual Brand Brand { get; set; }
        public virtual CategoryBase Category { get; set; }
        public DateTime CreationTime { get; private set; }
        public string MainImage { get; set; }
        public virtual FakeProductDiscount Discount { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ProductDiscountBase ProductDiscount { get; set; }
        public void Active() => IsActive = true;
        public void DeActive() => IsActive = false;
        public override void Validate()
        {
        }
    }
}