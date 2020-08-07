using System;
using Shopping.DomainModel.Aggregates.Orders.Entities;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities.Abstract
{
    public abstract class OrderSuggestionItemBase : Entity
    {
        protected OrderSuggestionItemBase()
        {
        }
        protected OrderSuggestionItemBase(Guid id, OrderItem orderItem)
        {
            Id = id;
            OrderItem = orderItem;
        }
        public virtual OrderItem OrderItem { get; private set; }
        public override void Validate()
        {
        }
    }
}