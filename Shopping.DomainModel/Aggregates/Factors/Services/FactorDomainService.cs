using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Factors.Interfaces;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Factors.Services
{
    public class FactorDomainService : IFactorDomainService
    {
        private readonly IRepository<Factor> _repository;
        public FactorDomainService(IRepository<Factor> repository)
        {
            _repository = repository;
        }
        public void FactorIsExist(long orderId)
        {
            var factor = _repository.AsQuery().SingleOrDefault(item => item.OrderId == orderId);
            if (factor != null)
            {
                throw new DomainException("فاکتور قبلا ثبت شده است");
            }
        }
        public bool HavePercentDiscountToday(Customer customer)
        {
            var now = DateTime.Now;
            var today = DateTime.Today;
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            return _repository.AsQuery()
                .Where(p => p.CreationTime >= startDateTime &&
                            p.CreationTime <= endDateTime &&
                            p.Customer.Id == customer.Id &&
                            p.FactorState == FactorState.Paid)
                .SelectMany(p => p.FactorRaws)
                .Any(p => p.Discount != null && p.Discount is FactorRawPercentDiscount &&
                          p.Discount.FromDate <= today &&
                          p.Discount.ToDate >= today &&
                          (p.Discount as FactorRawPercentDiscount).FromTime <= now.TimeOfDay &&
                          (p.Discount as FactorRawPercentDiscount).ToTime >= now.TimeOfDay);
        }
        public bool HasFirstBuy(Customer customer)
        {
            return _repository.AsQuery()
                .Any(p => p.Customer.Id == customer.Id && p.FactorState == FactorState.Paid);
        }
    }
}
