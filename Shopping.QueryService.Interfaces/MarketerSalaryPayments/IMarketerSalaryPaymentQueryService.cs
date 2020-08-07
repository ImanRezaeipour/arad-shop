using System;
using System.Linq;
using Shopping.QueryModel.QueryModels.MarketerSalaryPayments;

namespace Shopping.QueryService.Interfaces.MarketerSalaryPayments
{
    public interface IMarketerSalaryPaymentQueryService
    {
        IQueryable<IMarketerSalaryPaymentDto> GetAll();
        IMarketerSalaryPaymentDto GetById(Guid id);
    }
}