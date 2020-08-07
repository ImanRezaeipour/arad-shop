using System;
using Shopping.QueryModel.QueryModels.MarketerSalaryPayments;

namespace Shopping.QueryModel.Implements.MarketerSalaryPayments
{
    public class PeriodSalaryDto: IPeriodSalaryDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}