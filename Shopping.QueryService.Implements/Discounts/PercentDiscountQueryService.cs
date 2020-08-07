using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements;
using Shopping.QueryModel.Implements.Discounts;
using Shopping.QueryModel.Implements.Persons;
using Shopping.QueryModel.QueryModels.Discounts;
using Shopping.QueryService.Implements.Products;
using Shopping.QueryService.Interfaces.Discounts;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Discounts
{
    public class PercentDiscountQueryService : IPercentDiscountQueryService
    {
        private readonly IReadOnlyRepository<PercentDiscount, Guid> _percentRepository;
        private readonly IReadOnlyRepository<DiscountBase, Guid> _repository;
        private readonly IReadOnlyRepository<Customer, Guid> _customerRepository;
        private readonly IReadOnlyRepository<OrderBase, long> _orderRepository;
        private readonly IReadOnlyRepository<Factor, long> _factorRepository;
        public PercentDiscountQueryService(IReadOnlyRepository<PercentDiscount, Guid> percentRepository, IReadOnlyRepository<DiscountBase, Guid> repository, IReadOnlyRepository<Customer, Guid> customerRepository, IReadOnlyRepository<OrderBase, long> orderRepository, IReadOnlyRepository<Factor, long> factorRepository)
        {
            _percentRepository = percentRepository;
            _repository = repository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _factorRepository = factorRepository;
        }

        public async Task<IEnumerable<IProductDiscountDto>> GetProductPercentDiscountById(Guid percentDiscountId)
        {
            var discount = await _percentRepository.FindAsync(percentDiscountId);
            var result = discount.ProductDiscounts.Select(p => new ProductDiscountDto
            {
                UserInfo = new UserInfoDto
                {
                    UserId = p.UserInfo.UserId,
                    FirstName = p.UserInfo.FirstName,
                    LastName = p.UserInfo.LastName
                },
                CreationTime = p.CreationTime,
                Product = p.Product.ToProductWithDiscountDto()
            });
            return result;
        }

        public async Task<MobilePagingResultDto<IProductDiscountDto>> GetProductPercentDiscountByIdPaging(Guid percentDiscountId, PagedInputDto pagedInput)
        {
            var percentDiscount = _percentRepository.Find(percentDiscountId);
            if (percentDiscount == null)
            {
                throw new DomainException("تخفیف یافت نشد");
            }
            if (!(percentDiscount.FromDate <= DateTime.Today &&
                 percentDiscount.ToDate >= DateTime.Today))
            {
                throw new DomainException("تخفیف در این بازه زمانی روز موجود نمی باشد");
            }
            if (!(percentDiscount.FromTime <= DateTime.Now.TimeOfDay &&
                  percentDiscount.ToTime >= DateTime.Now.TimeOfDay))
            {
                throw new DomainException("تخفیف در این بازه زمانی ساعت موجود نمی باشد");
            }
            if (percentDiscount.RemainOrderCount <= 0)
            {
                throw new DomainException("تخفیف در این بازه زمانی ساعت موجود نمی باشد");
            }
            var data = _percentRepository.AsQuery().Where(item => item.Id == percentDiscountId)
                .SelectMany(item => item.ProductDiscounts);
            var result = await data.OrderByDescending(p => p.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToListAsync();
            return new MobilePagingResultDto<IProductDiscountDto>
            {
                Result = result.Select(item => item.ToDto())
                    .ToList(),
                Count = data.Count()
            };
        }

        public IPercentDiscountDto GetById(Guid percentDiscountId)
        {
            var result = _percentRepository.AsQuery().SingleOrDefault(p => p.Id == percentDiscountId);
            return result.ToDiscountDto();
        }

        public IQueryable<DiscountBaseDto> GetAll()
        {
            var result = _repository.AsQuery().Select(p => new DiscountBaseDto
            {
                Id = p.Id,
                UserInfo = new UserInfoDto
                {
                    UserId = p.UserInfo.UserId,
                    FirstName = p.UserInfo.FirstName,
                    LastName = p.UserInfo.LastName
                },
                CreationTime = p.CreationTime,
                Description = p.Description,
                FromDate = p.FromDate,
                ToDate = p.ToDate,
                Title = p.Title
            });
            return result;
        }

        public IQueryable<DiscountPercentShopSellDto> GetPercentDiscountShopSells(Guid discountId)
        {
            var discount = _percentRepository.Find(discountId);
            var shopSells = discount.Sells.GroupBy(item => item.Shop)
                .Select(y => new
                {
                    y.Key,
                    Debit = y.Sum(x => x.ShopDebitPrice)
                })
                .Select(item => new DiscountPercentShopSellDto
                {
                    Shop = new ShopDto
                    {
                        Name = item.Key.Name,
                        FirstName = item.Key.FirstName,
                        LastName = item.Key.LastName,
                        Id = item.Key.Id,
                        PersonNumber = item.Key.PersonNumber,
                        MobileNumber = item.Key.MobileNumber,
                        CreationTime = item.Key.CreationTime
                    },
                    Debit = item.Debit
                }).AsQueryable();
            return shopSells;
        }
        public PercentDiscountSumReportDto SumOfReport(Guid discountId)
        {
            var discount = _percentRepository.Find(discountId);
            var sells = discount.Sells.ToList();
            var countSells = sells.Count;
            var firstPurchaseCount = sells.Count(p => p.DiscountSellType == DiscountSellType.FirstPurchase);
            var remainOrderCount = discount.RemainOrderCount;
            var shopDebitSum = sells.Sum(item => item.ShopDebitPrice);
            var financialBenefitSum = sells.Sum(item => item.FinancialBenefit);
            return new PercentDiscountSumReportDto
            {
                SellsCount = countSells,
                RemainOrderCount = remainOrderCount,
                FinancialBenefitSum = financialBenefitSum,
                FirstPurchaseCount = firstPurchaseCount,
                ShopDebitSum = shopDebitSum
            };
        }

        public bool CheckUserTodayHavePercentDiscount(Guid userId)
        {
            var customer = _customerRepository.AsQuery().SingleOrDefault(item => item.UserId == userId);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            var now = DateTime.Now;
            var today = DateTime.Today;
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            return _factorRepository.AsQuery()
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
    }
}