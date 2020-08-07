using System;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.Products.Events
{
    public class UpdatePercentDiscountEvent : IDomainEvent
    {
        public UpdatePercentDiscountEvent(Guid discountId, string title, DateTime fromDate, DateTime toDate)
        {
            DiscountId = discountId;
            Title = title;
            FromDate = fromDate;
            ToDate = toDate;
            DomainEventDate = DateTime.Now;
        }
        public Guid DiscountId { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime DomainEventDate { get; set; }
    }
}