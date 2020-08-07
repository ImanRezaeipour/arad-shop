namespace Shopping.QueryModel.QueryModels.Persons.Shop.Charts
{
    public interface IShopFactorChart
    {
        string Label { get; set; }
        int TotalCount { get; set; }
        decimal TotalSum { get; set; }
    }
}