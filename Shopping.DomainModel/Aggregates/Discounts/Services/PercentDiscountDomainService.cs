using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Discounts.Entities;
using Shopping.DomainModel.Aggregates.Discounts.Interfaces;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Factors.Interfaces;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Discounts.Services
{
    public class PercentDiscountDomainService : IPercentDiscountDomainService
    {
        private readonly IRepository<PercentDiscount> _percentRepository;
        private readonly IRepository<DiscountBase> _repository;
        private readonly IRepository<Factor> _factorRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IFactorDomainService _factorDomainService;
        private readonly IRepository<OrderBase> _orderRepository;
        public PercentDiscountDomainService(IRepository<PercentDiscount> percentRepository, IRepository<Factor> factorRepository, IRepository<Product> productRepository, IFactorDomainService factorDomainService, IRepository<OrderBase> orderRepository, IRepository<DiscountBase> repository)
        {
            _percentRepository = percentRepository;
            _factorRepository = factorRepository;
            _productRepository = productRepository;
            _factorDomainService = factorDomainService;
            _orderRepository = orderRepository;
            _repository = repository;
        }
        public async Task CheckPercentDiscountDate(DateTime fromDate, DateTime toDate)
        {
            var any = await _percentRepository.AsQuery()
                .AnyAsync(p => (p.FromDate >= fromDate && p.ToDate <= fromDate) ||
                               (p.FromDate >= toDate && p.ToDate <= toDate) ||
                               (fromDate >= p.FromDate && toDate <= p.FromDate) ||
                               (fromDate >= p.ToDate && toDate <= p.ToDate));
            if (any)
            {
                throw new DomainException("در این بازه تخفیف در صدی موجود می باشد");
            }
        }
        public void ValidationSettingDiscount(int maxOrderCount, int discountMaxOrderCount, int maxProductCount,
            int discountMaxProductCount)
        {
            if (discountMaxOrderCount > maxOrderCount)
                throw new DomainException("تعدادسفارش ها برای این تخفیف از حداکثر سفارش  انتخابی بیشتر می باشد ");

            if (discountMaxProductCount > maxProductCount)
                throw new DomainException(" تعداد محصولات این تخفیف از حداکثر محصول انتخابی بیشتر است");
        }

        public bool HaveRemainOrderCount(Guid percentDiscountId, Customer customer)
        {
            if (_factorRepository.AsQuery().Any(p => p.FactorState == FactorState.Paid && p.Customer == customer))
            {
                return true;
            }
            var percentDiscount = _percentRepository.Find(percentDiscountId);
            return percentDiscount.RemainOrderCount >= 1;
        }
        public void LowOfNumberRemainOrderCount(Guid percenrDiscountId, Customer customer)
        {
            if (!_factorRepository.AsQuery()
                .Any(p => p.Customer.Id == customer.Id && p.FactorState == FactorState.Paid)) return;
            var percentDiscount = _percentRepository.Find(percenrDiscountId);
            percentDiscount.RemainOrderCount--;
        }

        public void AddDiscountSellToPercentDiscount(Factor factor)
        {
            var factorRaw = factor.FactorRaws.SingleOrDefault(p => p.Discount is FactorRawPercentDiscount);
            if (factorRaw != null)
            {
                var discount = _repository.AsQuery().SingleOrDefault(p => p.Id == factorRaw.Discount.DiscountId);
                var percentDiscount = discount as PercentDiscount;
                var product = _productRepository.AsQuery().SingleOrDefault(p => p.Id == factorRaw.ProductId);
                var order = _orderRepository.AsQuery().SingleOrDefault(p => p.Id == factor.OrderId);
                var discountSellType = _factorDomainService.HasFirstBuy(factor.Customer) ? DiscountSellType.Usual : DiscountSellType.FirstPurchase;
                var discountSell = new DiscountSell(Guid.NewGuid(), product, factor.Customer, discountSellType,
                    CalcShopDebitPrice(factorRaw.Price, factor.Discount, percentDiscount.Percent),
                    CalcFinancialBenefit(factorRaw.Price, factor.Discount, percentDiscount.Percent), order, factor.Shop, factor);
                discount.Sells.Add(discountSell);
            }
        }
        public decimal CalcFinancialBenefit(decimal price, int discount, float systemDiscount)
        {
            var shopDebitPrice = price * (100 - discount) / 100;
            var onePercent = (shopDebitPrice * (decimal)systemDiscount / 100) * (decimal)0.01;
            return decimal.Floor(onePercent);
        }
        private static decimal CalcShopDebitPrice(decimal price, int discount, float systemDiscount)
        {
            var shopPrice = price * (100 - discount) / 100;
            var shopDebitPrice = shopPrice * (decimal)systemDiscount / 100;
            shopDebitPrice = shopDebitPrice - (shopDebitPrice * (decimal)0.01);
            return decimal.Floor(shopDebitPrice);
        }
    }
}