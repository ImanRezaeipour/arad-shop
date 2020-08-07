using System;
using Shopping.DomainModel.Aggregates.Orders.Entities;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities.Abstract;

namespace Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities
{
    public class NoProductSuggestionItem : OrderSuggestionItemBase
    {
        protected NoProductSuggestionItem(){}
        public NoProductSuggestionItem(Guid id, OrderItem orderItem) : base(id, orderItem)
        {
        }
    }
}