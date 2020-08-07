using Shopping.QueryModel.QueryModels.Persons.Shop.CustomerSubsets;

namespace Shopping.QueryModel.Implements.Persons.CustomerSubsets
{
    public class ShopCustomerSubsetReportDto: IShopCustomerSubsetReportDto
    {
        public int Count { get; set; }
        public int PaidFactorCount { get; set; }
        public int SettlementCount { get; set; }
        public int NotSettlementCount { get; set; }
        public decimal ShopCustomerSubsetSumAmount { get; set; }
        public decimal ShopCustomerSubsetHaveFactorPaidSumAmount { get; set; }
    }
}