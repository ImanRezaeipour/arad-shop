using Shopping.QueryModel.QueryModels.Factors;

namespace Shopping.QueryModel.Implements.Factors
{
    public class FactorTotalReportFinancialDto: IFactorTotalReportFinancialDto
    {
        public decimal? SumSystemDiscount { get; set; }
        public decimal? SumDiscountPrice { get; set; }
        public decimal? SumRealDiscount { get; set; }
    }
}