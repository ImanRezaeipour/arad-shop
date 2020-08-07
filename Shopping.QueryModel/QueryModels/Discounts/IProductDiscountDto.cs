using System;
using Shopping.QueryModel.QueryModels.Products;

namespace Shopping.QueryModel.QueryModels.Discounts
{
    public interface IProductDiscountDto
    {
        DateTime CreationTime { get; set; }
        IUserInfoDto UserInfo { get; set; }
        IProductWithDiscountDto Product { get; set; }
    }
}