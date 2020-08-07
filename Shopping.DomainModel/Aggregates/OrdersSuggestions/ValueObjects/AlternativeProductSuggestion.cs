using System;
using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.OrdersSuggestions.ValueObjects
{
    public class AlternativeProductSuggestion : ValueObject<AlternativeProductSuggestion>
    {
        protected AlternativeProductSuggestion()
        {
        }
        public AlternativeProductSuggestion(Guid productId, string name, string productImage, Guid brandId, string brandName)
        {
            ProductId = productId;
            Name = name;
            ProductImage = productImage;
            BrandId = brandId;
            BrandName = brandName;
        }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public string ProductImage { get; private set; }
        public Guid BrandId { get;private set; }
        public string BrandName { get;private set; }
    }
}