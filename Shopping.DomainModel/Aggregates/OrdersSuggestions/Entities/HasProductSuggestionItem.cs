using System;
using Shopping.DomainModel.Aggregates.Orders.Entities;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities.Abstract;

namespace Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities
{
    public class HasProductSuggestionItem : OrderSuggestionItemBase
    {
        protected HasProductSuggestionItem() { }
        public HasProductSuggestionItem(Guid id, OrderItem orderItem, int quantity, string description, decimal price,
            bool isQuantityChanged) : base(id, orderItem)
        {
            Price = price;
            Quantity = quantity;
            Description = description;
            IsQuantityChanged = isQuantityChanged;
        }
        public bool IsQuantityChanged { get; private set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}