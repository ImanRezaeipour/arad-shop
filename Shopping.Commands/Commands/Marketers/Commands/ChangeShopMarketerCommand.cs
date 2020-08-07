using System;
using Shopping.Commands.Commands.Shared;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Marketers.Commands
{
    public class ChangeShopMarketerCommand : ShoppingCommandBase
    {
        public Guid ShopId { get; set; }
        public long NewMarketerId { get; set; }
        public UserInfoCommandItem UserInfo { get; set; }
    }
}