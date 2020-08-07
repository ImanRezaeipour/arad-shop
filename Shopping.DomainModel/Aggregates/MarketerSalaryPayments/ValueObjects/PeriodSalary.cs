using System;
using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.MarketerSalaryPayments.ValueObjects
{
    public class PeriodSalary : ValueObject<PeriodSalary>
    {
        protected PeriodSalary()
        { }
        public PeriodSalary(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}