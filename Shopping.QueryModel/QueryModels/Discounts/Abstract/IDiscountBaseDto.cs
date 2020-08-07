using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.Discounts.Abstract
{
    public interface IDiscountBaseDto
    {
        Guid Id { get; set; }
        string Description { get; set; }
        IUserInfoDto UserInfo { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        string Title { get; set; }
        DateTime CreationTime { get; set; }
        DiscountType DiscountType { get; set; }
    }
}