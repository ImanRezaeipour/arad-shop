namespace Shopping.QueryModel.Implements.Discounts
{
    public class PercentDiscountSumReportDto
    {
        public long SellsCount { get; set; }
        public long FirstPurchaseCount { get; set; }
        public int RemainOrderCount { get; set; }
        public decimal ShopDebitSum { get; set; }
        public decimal FinancialBenefitSum { get; set; }
    }
}