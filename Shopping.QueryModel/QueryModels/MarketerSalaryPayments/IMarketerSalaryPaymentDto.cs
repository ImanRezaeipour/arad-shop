using System;
using Shopping.QueryModel.QueryModels.Marketers;

namespace Shopping.QueryModel.QueryModels.MarketerSalaryPayments
{
    public interface IMarketerSalaryPaymentDto
    {
        Guid Id { get; set; }
        decimal Amount { get; set; }
        DateTime CreationTime { get; set; }
        IMarketerDto Marketer { get; set; }
        IPeriodSalaryDto PeriodSalary { get; set; }

    }
}