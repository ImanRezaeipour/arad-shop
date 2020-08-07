using System;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Categories.ValueObjects;

namespace Shopping.DomainModel.Aggregates.Categories.Aggregates
{
    public class CategoryRoot : CategoryBase
    {
        protected CategoryRoot() { }
        public CategoryRoot(Guid id, string name, string description, int order, CategoryImage imageCategory) : base(id, name, description, order, imageCategory)
        {
        }
    }
}