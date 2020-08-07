using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shopping.DomainModel.Aggregates.Discounts.Entities;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Discounts.Aggregates.Abstract
{
    public abstract class DiscountBase : AggregateRoot, IHasCreationTime
    {
        protected DiscountBase()
        { }
        protected DiscountBase(Guid id, string description, UserInfo userInfo, DateTime fromDate,
            DateTime toDate, string title)
        {
            if (toDate < fromDate) throw new DomainException(" تاریخ شروع و پایان نا معتبر");
            Id = id;
            Description = description;
            UserInfo = userInfo;
            FromDate = fromDate;
            ToDate = toDate;
            Title = title;
            CreationTime = DateTime.Now;
        }
        public virtual UserInfo UserInfo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Title { get; set; }
        public DateTime CreationTime { get; private set; }
        public string Description { get; set; }
        public virtual ICollection<DiscountSell> Sells { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Timestamp]
        public byte[] Timestamp { get; private set; }
        public override void Validate()
        {
        }
    }
}