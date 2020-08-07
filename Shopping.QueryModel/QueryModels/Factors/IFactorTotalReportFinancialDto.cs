namespace Shopping.QueryModel.QueryModels.Factors
{
    public interface IFactorTotalReportFinancialDto
    {
        decimal? SumSystemDiscount { get; set; }
        decimal? SumDiscountPrice { get; set; }
        decimal? SumRealDiscount { get; set; }
    }
}