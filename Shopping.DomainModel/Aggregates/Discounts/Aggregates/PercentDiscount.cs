using System;
using System.Collections.Generic;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Discounts.Entities;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core;

namespace Shopping.DomainModel.Aggregates.Discounts.Aggregates
{
    public class PercentDiscount : DiscountBase
    {
        protected PercentDiscount() { }
        public PercentDiscount(Guid id, string description, UserInfo userInfo, DateTime fromDate, DateTime toDate,
            string title, float percent, int maxOrderCount, int maxProductCount, TimeSpan fromTime, TimeSpan toTime) :
            base(id, description, userInfo, fromDate, toDate, title)
        {
            if (toTime < fromTime) throw new DomainException(" ساعت شروع و پایان نا معتبر");
            Percent = percent;
            MaxOrderCount = maxOrderCount;
            MaxProductCount = maxProductCount;
            FromTime = fromTime;
            ToTime = toTime;
            RemainOrderCount = maxOrderCount;
        }
        public float Percent { get; set; }
        public int MaxOrderCount { get; set; }
        public int MaxProductCount { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public int RemainOrderCount { get; set; }
        public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; }
    }
}