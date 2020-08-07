namespace Shopping.QueryModel.QueryModels.Dashboards
{
    public interface IDashboardCountDto
    {
        long CustomerActiveCount { get; set; }
        long ShopActiveCount { get; set; }
        long FactorPaidCount { get; set; }
        long FactorPendingCount { get; set; }
        long ShopDeActiveCount { get; set; }
        long CustomerDeActiveCount { get; set; }
    }
}