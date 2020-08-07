using System;

namespace Shopping.QueryModel.QueryModels.Factors
{
    public interface IFactorReportFinancialDto
    {
        long Id { get; set; }
        DateTime CreationTime { get; set; }
        string CustomerFirstName { get; set; }
        string CustomerLastName { get; set; }
        string ShopName { get; set; }
        //مبلغ نا خالص
        decimal DiscountPrice { get; set; }
        //مبلغ خالص واریز بانک
        decimal SystemDiscountPrice { get; set; }
        // مبلغ تخفیف
        decimal RealDiscountPrice { get; set; }
        string ShopIban { get; set; }
    }
}