using System;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.DiscountRules.Interfaces;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Orders.DiscountRules.Rules
{
    public class OrderPercentDiscountRule : IOrderDiscountRule
    {
        private readonly IRepository<Factor> _factorRepository;
        public OrderPercentDiscountRule(IRepository<Factor> factorRepository)
        {
            _factorRepository = factorRepository;
        }
        public void CheckCustomerDiscount(Customer customer)
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            //var any=_factorRepository.AsQuery()
            //    .Where(p => p.CreationTime >= startDateTime &&
            //                p.CreationTime <= endDateTime &&
            //                p.Customer.Id == customer.Id)
            //    .SelectMany(p => p.FactorRaws)
            //    .Any(p => p.Discount != null && p.Discount is FactorRawPercentDiscount);
            //if (any)
            //{
            //    throw new DomainException("شما امروز از این تخفیف استفاده کرده اید");
            //}


            //var any = _orderRepository.AsQuery()
            //    .Where(p => p.CreationTime >= startDateTime &&
            //                p.CreationTime <= endDateTime &&
            //                p.Customer.Id == customer.Id)
            //    .SelectMany(p => p.OrderItems)
            //    .Any(p => p.Discount != null && p.Discount is OrderItemPercentDiscount);
            //if (any)
            //{
            //    throw new DomainException("شما امروز از این تخفیف استفاده کرده اید");
            //}
        }
    }
}