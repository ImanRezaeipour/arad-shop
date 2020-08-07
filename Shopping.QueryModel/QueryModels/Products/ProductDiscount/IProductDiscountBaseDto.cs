using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.Products.ProductDiscount
{
    public interface IProductDiscountBaseDto
    {
        Guid Id { get; set; }
        string Title { get; set; }
        IUserInfoDto UserInfo { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        DiscountType DiscountType { get; set; }
    }
}