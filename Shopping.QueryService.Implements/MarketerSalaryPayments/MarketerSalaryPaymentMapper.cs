using AutoMapper;
using Shopping.DomainModel.Aggregates.MarketerSalaryPayments.Aggregates;
using Shopping.QueryModel.QueryModels.MarketerSalaryPayments;

namespace Shopping.QueryService.Implements.MarketerSalaryPayments
{
    public static class MarketerSalaryPaymentMapper
    {
        public static IMarketerSalaryPaymentDto ToDto(this MarketerSalaryPayment src)
        {
            return Mapper.Map<IMarketerSalaryPaymentDto>(src);
        }
    }
}