using System;
using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.ProductsSuggestions.ValueObjects
{
    public class ProductSuggestionGroup : ValueObject<ProductSuggestionGroup>
    {
        protected ProductSuggestionGroup() { }
        public ProductSuggestionGroup(Guid categoryRootId, string categoryRootName, Guid categoryId, string categoryName, Guid brandId, string brandName)
        {
            CategoryRootId = categoryRootId;
            CategoryRootName = categoryRootName;
            CategoryId = categoryId;
            CategoryName = categoryName;
            BrandId = brandId;
            BrandName = brandName;
        }
        public Guid CategoryRootId { get; private set; }
        public string CategoryRootName { get; private set; }
        public Guid CategoryId { get; private set; }
        public string CategoryName { get; private set; }
        public Guid BrandId { get; private set; }
        public string BrandName { get; private set; }
    }
}