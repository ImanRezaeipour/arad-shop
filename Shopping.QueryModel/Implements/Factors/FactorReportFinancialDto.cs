using System;
using Shopping.QueryModel.QueryModels.Factors;

namespace Shopping.QueryModel.Implements.Factors
{
    public class FactorReportFinancialDto : IFactorReportFinancialDto
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string ShopName { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal SystemDiscountPrice { get; set; }
        public decimal RealDiscountPrice { get; set; }
        public string ShopIban { get; set; }
    }
}