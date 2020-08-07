using System;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.ShopOrderLogs.Aggregates
{
    public class ShopOrderLog : AggregateRoot, IHasCreationTime
    {
        protected ShopOrderLog() { }
        public ShopOrderLog(Guid id, Shop shop, OrderBase order)
        {
            Id = id;
            Shop = shop;
            Order = order;
            CreationTime = DateTime.Now;
            HasSuggestions = false;
            HasFactor = false;
        }
        public virtual Shop Shop { get; private set; }
        public virtual OrderBase Order { get; private set; }
        public virtual OrderSuggestion OrderSuggestion { get; set; }
        public virtual Factor Factor { get; set; }
        public DateTime CreationTime { get; private set; }
        public bool HasSuggestions { get; set; }
        public DateTime? DateHasSuggestions { get; set; }
        public bool HasFactor { get; set; }
        public DateTime? DateHasFactor { get; set; }
        public override void Validate()
        {
        }
    }
}