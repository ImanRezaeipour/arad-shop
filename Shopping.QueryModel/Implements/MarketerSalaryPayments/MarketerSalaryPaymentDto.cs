using System;
using Shopping.QueryModel.QueryModels.Marketers;
using Shopping.QueryModel.QueryModels.MarketerSalaryPayments;

namespace Shopping.QueryModel.Implements.MarketerSalaryPayments
{
    public class MarketerSalaryPaymentDto: IMarketerSalaryPaymentDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreationTime { get; set; }
        public IMarketerDto Marketer { get; set; }
        public IPeriodSalaryDto PeriodSalary { get; set; }
    }
}