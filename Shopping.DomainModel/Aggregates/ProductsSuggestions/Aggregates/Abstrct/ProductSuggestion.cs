using System;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.ValueObjects;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates.Abstrct
{
    public abstract class ProductSuggestion : AggregateRoot, IHasCreationTime
    {
        protected ProductSuggestion() { }
        protected ProductSuggestion(Guid id, string title, string productImage, string description, Guid personId, ProductSuggestionGroup productSuggestionGroup)
        {
            Id = id;
            Title = title;
            ProductImage = productImage;
            ProductSuggestionStatus = ProductSuggestionStatus.Pending;
            CreationTime = DateTime.Now;
            Description = description;
            PersonId = personId;
            ProductSuggestionGroup = productSuggestionGroup;
        }
        public string Title { get; set; }
        public string ProductImage { get; set; }
        public ProductSuggestionStatus ProductSuggestionStatus { get; set; }
        public string ProductSuggestionStatusDescription { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; private set; }
        public Guid PersonId { get; set; }
        public virtual ProductSuggestionGroup ProductSuggestionGroup { get; private set; }
    }
}