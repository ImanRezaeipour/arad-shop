using System;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.ShopMarketersHistories.Aggregates
{
    /// <summary>
    /// تاریخچه بازاریاب فروشگاه
    /// </summary>
    public class ShopMarketersHistory : AggregateRoot, IHasCreationTime
    {
        protected ShopMarketersHistory()
        {
        }
        public ShopMarketersHistory(Guid id, Shop shop, Marketer marketer, ShopMarketersHistoryType shopMarketersHistoryType, UserInfo userInfo)
        {
            Id = id;
            Shop = shop;
            Marketer = marketer;
            ShopMarketersHistoryType = shopMarketersHistoryType;
            UserInfo = userInfo;
            CreationTime = DateTime.Now;
        }
        public DateTime CreationTime { get; private set; }
        public virtual Shop Shop { get; private set; }
        public virtual Marketer Marketer { get; private set; }
        public ShopMarketersHistoryType ShopMarketersHistoryType { get; private set; }
        public virtual UserInfo UserInfo { get; private set; }
        public override void Validate()
        {
        }
    }
}