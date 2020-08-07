using System;
using System.Collections.Generic;
using Shopping.DomainModel.Aggregates.Categories.ValueObjects;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract
{
    public abstract class CategoryBase : AggregateRoot<Guid>, IPassivable
    {
        protected CategoryBase()
        {
        }
        protected CategoryBase(Guid id, string name, string description, int order, CategoryImage imageCategory)
        {
            Id = id;
            Name = name;
            Description = description;
            Order = order;
            CategoryImage = imageCategory;
            IsActive = true;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; private set; }
        public int Order { get; set; }
        public void Active() => IsActive = true;
        public void DeActive() => IsActive = false;
        public virtual CategoryImage CategoryImage { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public override void Validate()
        {
        }
    }
}