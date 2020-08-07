using System;
using System.Collections.Generic;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels;
using Shopping.QueryModel.QueryModels.Discounts;

namespace Shopping.QueryModel.Implements.Discounts
{
    public class PercentDiscountWithProductDto : IPercentDiscountWithProductDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public IUserInfoDto UserInfo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Title { get; set; }
        public DateTime CreationTime { get; set; }
        public DiscountType DiscountType { get; set; }
        public float Percent { get; set; }
        public IList<IProductDiscountDto> ProductDiscounts { get; set; }
    }
}