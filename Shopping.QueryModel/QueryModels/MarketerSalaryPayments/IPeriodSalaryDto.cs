using System;

namespace Shopping.QueryModel.QueryModels.MarketerSalaryPayments
{
    public interface IPeriodSalaryDto
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}