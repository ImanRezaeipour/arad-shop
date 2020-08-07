using Shopping.QueryModel.QueryModels.Persons.Shop.Charts;

namespace Shopping.QueryModel.Implements.Persons.Charts
{
    public class ShopFactorChart : IShopFactorChart
    {
        public string Label { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalSum { get; set; }
    }
}