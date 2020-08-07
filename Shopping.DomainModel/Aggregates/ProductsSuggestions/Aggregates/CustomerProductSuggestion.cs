using System;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates.Abstrct;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.ValueObjects;

namespace Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates
{
    public class CustomerProductSuggestion : ProductSuggestion
    {
        protected CustomerProductSuggestion() { }

        public CustomerProductSuggestion(Guid id, string title, string productImage, string description, Guid personId, ProductSuggestionGroup productSuggestionGroup) : base(id, title, productImage, description, personId, productSuggestionGroup)
        {
        }

        public override void Validate()
        {
        }
    }
}