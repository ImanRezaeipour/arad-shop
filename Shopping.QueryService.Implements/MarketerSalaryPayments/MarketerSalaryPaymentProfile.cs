using AutoMapper;
using Shopping.DomainModel.Aggregates.MarketerSalaryPayments.Aggregates;
using Shopping.DomainModel.Aggregates.MarketerSalaryPayments.ValueObjects;
using Shopping.QueryModel.QueryModels.MarketerSalaryPayments;

namespace Shopping.QueryService.Implements.MarketerSalaryPayments
{
    public class MarketerSalaryPaymentProfile:Profile
    {
        public MarketerSalaryPaymentProfile()
        {
            CreateMap<MarketerSalaryPayment, IMarketerSalaryPaymentDto>();
            CreateMap<PeriodSalary, IPeriodSalaryDto>();
        }
    }
}