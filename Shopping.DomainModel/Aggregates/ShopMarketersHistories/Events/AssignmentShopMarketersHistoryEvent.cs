using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core.DomainEvent;

namespace Shopping.DomainModel.Aggregates.ShopMarketersHistories.Events
{
    public class AssignmentShopMarketersHistoryEvent : DomainEvent
    {
        public AssignmentShopMarketersHistoryEvent(Shop shop, Marketer marketer, UserInfo userInfo)
        {
            Shop = shop;
            Marketer = marketer;
            UserInfo = userInfo;
        }
        public Shop Shop { get; private set; }
        public Marketer Marketer { get; private set; }
        public UserInfo UserInfo { get;private set; }
    }
}