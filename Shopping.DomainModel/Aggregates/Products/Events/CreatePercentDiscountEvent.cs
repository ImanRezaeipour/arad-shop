using System;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Products.Events
{
    public class CreatePercentDiscountEvent : IDomainEvent
    {
        public CreatePercentDiscountEvent(Guid discountId, Guid productId, string title, UserInfo userInfo, DateTime fromDate, DateTime toDate, float percent, TimeSpan fromTime, TimeSpan toTime, DiscountType discountType)
        {
            DiscountId = discountId;
            ProductId = productId;
            Title = title;
            UserInfo = userInfo;
            FromDate = fromDate;
            ToDate = toDate;
            DiscountType = discountType;
            Percent = percent;
            DomainEventDate = DateTime.Now;
            FromTime = fromTime;
            ToTime = toTime;
        }

        public DiscountType DiscountType { get; set; }
        public Guid DiscountId { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public float Percent { get; set; }
        public DateTime DomainEventDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}