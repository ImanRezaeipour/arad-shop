using Shopping.QueryModel.QueryModels.Marketers.Charts;

namespace Shopping.QueryModel.Implements.Marketers
{
    public class MarketerShopFactorChart: IMarketerShopFactorChart
    {
        public string Label { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalSum { get; set; }
    }
}