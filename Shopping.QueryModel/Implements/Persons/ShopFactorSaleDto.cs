using System;
using Shopping.QueryModel.QueryModels.Persons.Shop;

namespace Shopping.QueryModel.Implements.Persons
{
    public class ShopFactorSaleDto : IShopFactorSaleDto
    {
        public Guid id { get; set; }
        public long personNumber { get; set; }
        public string name { get; set; }
        public DateTime creationTime { get; set; }
        public long orderCount { get; set; }
        public long orderSugesstionCount { get; set; }
        public long factorCount { get; set; }
        public decimal? factorSum { get; set; }
        public int? pastDateCount { get; set; }
    }
}