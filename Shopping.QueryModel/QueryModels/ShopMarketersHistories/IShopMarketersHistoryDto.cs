using System;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.Marketers;

namespace Shopping.QueryModel.QueryModels.ShopMarketersHistories
{
    public interface IShopMarketersHistoryDto
    {
        Guid Id { get; set; }
        IMarketerDto Marketer { get; set; }
        IUserInfoDto UserInfo { get; set; }
        ShopMarketersHistoryType ShopMarketersHistoryType { get; set; }
        DateTime CreationTime { get;  set; }
    }
}