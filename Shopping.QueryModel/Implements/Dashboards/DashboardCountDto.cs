using Shopping.QueryModel.QueryModels.Dashboards;

namespace Shopping.QueryModel.Implements.Dashboards
{
    public class DashboardCountDto: IDashboardCountDto
    {
        public long CustomerActiveCount { get; set; }
        public long CustomerDeActiveCount { get; set; }
        public long ShopActiveCount { get; set; }
        public long ShopDeActiveCount { get; set; }
        public long FactorPaidCount { get; set; }
        public long FactorPendingCount { get; set; }
    }
}