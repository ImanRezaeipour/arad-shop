using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.MarketerSalaryPayments.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.QueryModel.Implements.Marketers;
using Shopping.QueryModel.Implements.MarketerSalaryPayments;
using Shopping.QueryModel.QueryModels.MarketerSalaryPayments;
using Shopping.QueryService.Interfaces.MarketerSalaryPayments;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.MarketerSalaryPayments
{
    public class MarketerSalaryPaymentQueryService: IMarketerSalaryPaymentQueryService
    {
        private readonly IReadOnlyRepository<MarketerSalaryPayment,Guid> _repository;

        public MarketerSalaryPaymentQueryService(IReadOnlyRepository<MarketerSalaryPayment, Guid> repository)
        {
            _repository = repository;
        }
        public IQueryable<IMarketerSalaryPaymentDto> GetAll()
        {
            var marketerSalaryPayment = _repository.AsQuery();
            var result = marketerSalaryPayment.Select(p => new MarketerSalaryPaymentDto
            {
                Id = p.Id,
                Amount = p.Amount /10,
                CreationTime = p.CreationTime,
                Marketer = new MarketerDto
                {
                    Id = p.Marketer.Id,
                    FirstName = p.Marketer.FirstName,
                    LastName = p.Marketer.LastName,
                    MaxMarketerAllowed = p.Marketer.MaxMarketerAllowed,
                    CreationTime = p.Marketer.CreationTime,
                    IsActive = p.Marketer.IsActive,
                },
                PeriodSalary = new PeriodSalaryDto
                {
                    FromDate = p.PeriodSalary.FromDate,
                    ToDate = p.PeriodSalary.ToDate
                },

            });
            return result;

        }

        public IMarketerSalaryPaymentDto GetById(Guid id)
        {
            var result = _repository.AsQuery().SingleOrDefault(p => p.Id == id);
            if (result== null)
            {
                throw new DomainException("پرداخت بازار یاف ت نشد");
            }
            return result.ToDto();
        }
    }
}