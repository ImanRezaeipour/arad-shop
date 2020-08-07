using System;
using Shopping.Commands.Commands.Shared;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Marketers.Commands
{
    public class DetachedMarketerShopCommand : ShoppingCommandBase
    {
        public long MarketerId { get; set; }
        public Guid ShopId { get; set; }
        public UserInfoCommandItem UserInfo { get; set; }
    }
}