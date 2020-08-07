using System;
using Shopping.QueryModel.QueryModels;
using Shopping.QueryModel.QueryModels.Discounts;
using Shopping.QueryModel.QueryModels.Products;

namespace Shopping.QueryModel.Implements.Discounts
{
    public class ProductDiscountDto: IProductDiscountDto
    {
        public DateTime CreationTime { get; set; }
        public IUserInfoDto UserInfo { get; set; }
        public IProductWithDiscountDto Product { get; set; }
    }
}