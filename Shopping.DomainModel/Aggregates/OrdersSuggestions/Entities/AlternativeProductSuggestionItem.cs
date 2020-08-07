using System;
using Shopping.DomainModel.Aggregates.Orders.Entities;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities.Abstract;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.ValueObjects;

namespace Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities
{
    public class AlternativeProductSuggestionItem : OrderSuggestionItemBase
    {
        protected AlternativeProductSuggestionItem() { }
        public AlternativeProductSuggestionItem(Guid id, OrderItem orderItem, int quantity, string description, decimal price, AlternativeProductSuggestion alternativeProductSuggestion,
            bool isQuantityChanged) : base(id, orderItem)
        {
            AlternativeProductSuggestion = alternativeProductSuggestion;
            Price = price;
            Quantity = quantity;
            Description = description;
            IsQuantityChanged = isQuantityChanged;
        }
        public int Quantity { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool IsQuantityChanged { get; private set; }
        public virtual AlternativeProductSuggestion AlternativeProductSuggestion { get; private set; }
    }
}