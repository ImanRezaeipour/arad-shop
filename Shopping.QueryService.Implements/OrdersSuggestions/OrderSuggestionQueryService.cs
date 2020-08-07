using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.OrdersSuggestions;
using Shopping.QueryService.Interfaces.OrdersSuggestions;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.OrdersSuggestions
{
    public class OrderSuggestionQueryService : IOrderSuggestionQueryService
    {
        private readonly IReadOnlyRepository<OrderSuggestion, Guid> _repository;
        private readonly IReadOnlyRepository<Factor, long> _factorRepository;
        private readonly IReadOnlyRepository<OrderBase, long> _orderRepository;
        public OrderSuggestionQueryService(IReadOnlyRepository<OrderSuggestion, Guid> repository, IReadOnlyRepository<Factor, long> factorRepository, IReadOnlyRepository<OrderBase, long> orderRepository)
        {
            _repository = repository;
            _factorRepository = factorRepository;
            _orderRepository = orderRepository;
        }
        public IOrderSuggestionDto GetByOrderId(long orderId)
        {
            var order = _orderRepository.Find(orderId);
            var now = DateTime.Now;
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            var havePercentDiscountToday = _factorRepository.AsQuery()
                 .Where(p => p.CreationTime >= startDateTime &&
                             p.CreationTime <= endDateTime &&
                             p.Customer.Id == order.Customer.Id &&
                             p.FactorState == FactorState.Paid)
                 .SelectMany(p => p.FactorRaws)
                 .Any(p => p.Discount != null && p.Discount is FactorRawPercentDiscount &&
                           p.Discount.FromDate <= now &&
                           p.Discount.ToDate >= now &&
                           (p.Discount as FactorRawPercentDiscount).FromTime <= now.TimeOfDay &&
                           (p.Discount as FactorRawPercentDiscount).ToTime >= now.TimeOfDay);
            //todo autho
            var t = _repository.AsQuery().Where(p => p.OrderId == orderId).ToList();
            var orderSuggestion = _repository.AsQuery().SingleOrDefault(p => p.OrderId == orderId);

            if (!havePercentDiscountToday) return orderSuggestion.ToDo();
            if (orderSuggestion == null) return ((OrderSuggestion) null).ToDo();
            foreach (var item in orderSuggestion.OrderSuggestionItems)
            {
                item.OrderItem.Discount = null;
            }
            return  orderSuggestion.ToDo();
         
        }

        public async Task<IOrderSuggestionDto> GetAcceptedOrderSuggestion(long orderId)
        {
            var orderSuggestion =await _repository.AsQuery().SingleOrDefaultAsync(item =>
                item.OrderId == orderId && item.OrderSuggestionStatus == OrderSuggestionStatus.Accept);
            if (orderSuggestion == null)
            {
                throw new DomainException("پیشنهاد سفارش یافت نشد");
            }
            return orderSuggestion.ToDo();
        }
    }
}