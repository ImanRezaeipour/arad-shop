namespace Shopping.QueryModel.QueryModels.Marketers.Charts
{
    public interface IMarketerShopFactorChart
    {
        string Label { get; set; }
        int TotalCount { get; set; }
        decimal TotalSum { get; set; }
    }
}