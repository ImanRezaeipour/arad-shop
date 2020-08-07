using System;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates.Abstrct;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.ValueObjects;

namespace Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates
{
    public class ShopProductSuggestion : ProductSuggestion
    {
        protected ShopProductSuggestion() { }
        public ShopProductSuggestion(Guid id, string title, string productImage, string description, Guid personId, ProductSuggestionGroup productSuggestionGroup) : base(id, title, productImage, description, personId, productSuggestionGroup)
        {
        }
        public override void Validate()
        {
        }
    }
}