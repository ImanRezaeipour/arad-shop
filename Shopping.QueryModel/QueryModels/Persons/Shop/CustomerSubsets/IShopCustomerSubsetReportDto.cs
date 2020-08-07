namespace Shopping.QueryModel.QueryModels.Persons.Shop.CustomerSubsets
{
    public interface IShopCustomerSubsetReportDto
    {
        /// <summary>
        /// تعداد کل مشتری جذب شده
        /// </summary>
        int Count { get; set; }
        /// <summary>
        /// تعداد مشتری جذب شده و منجر به خرید شده
        /// </summary>
        int PaidFactorCount { get; set; }
        /// <summary>
        /// تعداد تسویه نشده 
        /// </summary>
        int SettlementCount { get; set; }
        /// <summary>
        /// تعداد تسویه شده
        /// </summary>
        int NotSettlementCount { get; set; }
        /// <summary>
        /// مجموع کارمزد فروشگاه برای جذب شده 
        /// </summary>
        decimal ShopCustomerSubsetSumAmount { get; set; }
        /// <summary>
        /// مجموع کارمزد فروشگاه جذب شده های منجر به خرید تسویه نشده 
        /// </summary>
        decimal ShopCustomerSubsetHaveFactorPaidSumAmount { get; set; }
    }
}