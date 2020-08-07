using System;

namespace Shopping.QueryModel.QueryModels.Persons.Shop
{
    public interface IShopFactorSaleDto
    {
        Guid id { get; set; }
        long personNumber { get; set; }
        string name { get; set; }
        DateTime creationTime { get; set; }
        long orderCount { get; set; }
        long orderSugesstionCount { get; set; }
        long factorCount { get; set; }
        decimal? factorSum { get; set; }
        int? pastDateCount { get; set; }
    }
}