using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Orders.DiscountRules.Interfaces;
using Shopping.DomainModel.Aggregates.Orders.DiscountRules.Rules;
using Shopping.DomainModel.Aggregates.Orders.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Orders.Interfaces;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.Enums;
using Shopping.Repository.Write.Interface;
using Shopping.Shared.Enums;
using Shopping.Shared.Events.Interfaces.Users;
using Shopping.Shared.Events.Messages.Users;

namespace Shopping.DomainModel.Aggregates.Orders.Services
{
    public class OrderDomainService : IOrderDomainService
    {
        private readonly List<IOrderDiscountRule> _orderDiscountRule;
        private readonly IRepository<OrderBase> _orderRepository;
        private readonly IRepository<PercentDiscount> _percentDiscountRepository;
        private readonly IRepository<Factor> _factorRepository;
        private readonly IBus _bus;
        private readonly IContext _context;
        public OrderDomainService(IRepository<OrderBase> orderRepository, IRepository<PercentDiscount> percentRepository, IRepository<Factor> factorRepository, IBus bus, IContext context)
        {
            _orderDiscountRule = new List<IOrderDiscountRule>();
            _orderRepository = orderRepository;
            _percentDiscountRepository = percentRepository;
            _factorRepository = factorRepository;
            _bus = bus;
            _context = context;
        }
        public void CheckOrderDiscount(Customer customer)
        {
            _orderDiscountRule.Add(new OrderPercentDiscountRule(_factorRepository));
            foreach (var orderDiscountRule in _orderDiscountRule)
            {
                orderDiscountRule.CheckCustomerDiscount(customer);
            }
        }
        public OrderBase CalcOrderPercentDiscount(OrderBase order)
        {
            var now = DateTime.Now;
            var activePercentDiscount = _percentDiscountRepository.AsQuery()
                .SingleOrDefault(p => p.FromDate <= now &&
                                      p.ToDate >= now &&
                                      p.FromTime <= now.TimeOfDay &&
                                      p.ToTime >= now.TimeOfDay);
            if (activePercentDiscount == null)
            {
                return order;
            }
            var productDiscountCount = 0;
            foreach (var orderItem in order.OrderItems)
            {
                var isExist = activePercentDiscount.ProductDiscounts.SingleOrDefault(item =>
                    item.Product.Id == orderItem.OrderProduct.ProductId);
                if (isExist != null)
                {
                    ++productDiscountCount;
                }
            }
            if (productDiscountCount > 1)
            {
                throw new DomainException("تعداد کالاهای این تخفیف بیشتر از یک می باشد");
            }
            return order;
        }
        public bool HavePercentDiscountToday(Customer customer)
        {
            var now = DateTime.Now;
            var today = DateTime.Today;
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            return _orderRepository.AsQuery()
                .Where(p => p.CreationTime >= startDateTime &&
                            p.CreationTime <= endDateTime &&
                            p.Customer.Id == customer.Id)
                .SelectMany(p => p.OrderItems)
                .Any(p => p.Discount != null && p.Discount is OrderItemPercentDiscount &&
                          p.Discount.FromDate <= today &&
                          p.Discount.ToDate >= today &&
                          (p.Discount as OrderItemPercentDiscount).FromTime <= now.TimeOfDay &&
                          (p.Discount as OrderItemPercentDiscount).ToTime >= now.TimeOfDay);
        }

        public int CountPercentDiscountToday(Customer customer)
        {
            var now = DateTime.Now;
            var today = DateTime.Today;
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            return _orderRepository.AsQuery()
                .Where(p => p.CreationTime >= startDateTime &&
                            p.CreationTime <= endDateTime &&
                            p.Customer.Id == customer.Id)
                .SelectMany(p => p.OrderItems)
                .Count(p => p.Discount != null && p.Discount is OrderItemPercentDiscount &&
                          p.Discount.FromDate <= today &&
                          p.Discount.ToDate >= today &&
                          (p.Discount as OrderItemPercentDiscount).FromTime <= now.TimeOfDay &&
                          (p.Discount as OrderItemPercentDiscount).ToTime >= now.TimeOfDay);
        }

        public bool CheckOrderPercentDiscountToday(Customer customer, long orderId)
        {
            var now = DateTime.Now;
            var today = DateTime.Today;
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            return _orderRepository.AsQuery()
                .Where(p => p.CreationTime >= startDateTime &&
                            p.CreationTime <= endDateTime &&
                            p.Customer.Id == customer.Id &&
                            p.Id == orderId)
                .SelectMany(p => p.OrderItems)
                .Any(p => p.Discount != null && p.Discount is OrderItemPercentDiscount &&
                            p.Discount.FromDate <= today &&
                            p.Discount.ToDate >= today &&
                            (p.Discount as OrderItemPercentDiscount).FromTime <= now.TimeOfDay &&
                            (p.Discount as OrderItemPercentDiscount).ToTime >= now.TimeOfDay);

        }

        public async Task CheckCustomerRequestOrderDuration(Customer customer, ApplicationSetting setting)
        {
            var now = DateTime.Now;
            var fromTime = now.AddSeconds(-setting.CustomerRequestOrderDuration);
            var orderCountsInDuration = _orderRepository.AsQuery().Count(p =>
                p.Customer.Id == customer.Id && p.CreationTime >= fromTime && p.CreationTime <= now);
            if (orderCountsInDuration > setting.CustomerRequestOrderCount)
            {
                customer.DeActive();
                _context.SaveChanges();
                await _bus.Publish<IDeActiveUserEvent>(new DeActiveUserEvent(customer.UserId, AppType.Customer));
                throw new DomainException(
                    "تعداد سفارش های درخواستی شما بیشتر از حد مجاز می باشد. کاربر شما غیر فعال شده است!",
                    ErrorCode.UserDeActivated);
            }
        }
    }
}