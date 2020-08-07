using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PersianDate;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.BaseEntities.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.ShopOrderLogs.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.Helper;
using Shopping.QueryModel.Implements;
using Shopping.QueryModel.Implements.Persons;
using Shopping.QueryModel.Implements.Persons.Charts;
using Shopping.QueryModel.Implements.Persons.CustomerSubsets;
using Shopping.QueryModel.QueryModels.Persons.Shop;
using Shopping.QueryModel.QueryModels.Persons.Shop.Charts;
using Shopping.QueryModel.QueryModels.Persons.Shop.CustomerSubsets;
using Shopping.QueryService.Interfaces.Persons;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Persons
{
    public class ShopQueryService : IShopQueryService
    {
        private readonly IReadOnlyRepository<Shop, Guid> _repository;
        private readonly IReadOnlyRepository<Customer, Guid> _customerRepository;
        private readonly IReadOnlyRepository<City, Guid> _cityRepository;
        private readonly IReadOnlyRepository<ShopOrderLog, Guid> _shopOrderLogRepository;
        private readonly IReadOnlyRepository<Factor, long> _factorRepository;
        private readonly IReadOnlyRepository<Marketer, long> _marketerRepository;
        private readonly IReadOnlyRepository<ApplicationSetting, Guid> _applicationSettingRepository;
        public ShopQueryService(IReadOnlyRepository<Shop, Guid> repository,
            IReadOnlyRepository<City, Guid> cityRepository,
            IReadOnlyRepository<Customer, Guid> customerRepository,
            IReadOnlyRepository<ShopOrderLog, Guid> shopOrderLogRepository,
            IReadOnlyRepository<Factor, long> factorRepository, IReadOnlyRepository<Marketer, long> marketerRepository, IReadOnlyRepository<ApplicationSetting, Guid> applicationSettingRepository)
        {
            _repository = repository;
            _cityRepository = cityRepository;
            _customerRepository = customerRepository;
            _shopOrderLogRepository = shopOrderLogRepository;
            _factorRepository = factorRepository;
            _marketerRepository = marketerRepository;
            _applicationSettingRepository = applicationSettingRepository;
        }
        public IShopFullInfo GetByUserId(Guid userId)
        {
            var result = _repository.AsQuery().SingleOrDefault(item => item.UserId == userId);
            return result.ToShopFullInfoDto();
        }
        public IQueryable<ShopDto> GetAll()
        {
            var marketers = _marketerRepository.AsQuery();
            var shops = _repository.AsQuery();
            var join = shops.GroupJoin(marketers, s => s.MarketerId, m => m.Id,
                    (shop, marketer) => new
                    {
                        Shop = shop,
                        Marketer = marketer
                    })
                    .SelectMany(x => x.Marketer.DefaultIfEmpty(),
                    (x, y) => new
                    {
                        x.Shop,
                        Marketer = y
                    });

            var result = join
                .Select(item => new ShopDto
                {
                    Id = item.Shop.Id,
                    UserId = item.Shop.UserId,
                    IsActive = item.Shop.IsActive,
                    Description = item.Shop.Description,
                    Name = item.Shop.Name,
                    EmailAddress = item.Shop.EmailAddress,
                    DefaultDiscount = item.Shop.DefaultDiscount,
                    DescriptionStatus = item.Shop.DescriptionStatus,
                    FirstName = item.Shop.FirstName,
                    LastName = item.Shop.LastName,
                    NationalCode = item.Shop.NationalCode,
                    ShopStatus = item.Shop.ShopStatus,
                    MobileNumber = item.Shop.MobileNumber,
                    CreationTime = item.Shop.CreationTime,
                    PersonNumber = item.Shop.PersonNumber,
                    HasMarketer = item.Shop.MarketerId == null,
                    MarketerId = item.Shop.MarketerId,
                    Metrage = item.Shop.Metrage,
                    AreaRadius = item.Shop.AreaRadius,
                    MarketerFullName = item.Marketer.FirstName + " " + item.Marketer.LastName
                });
            return result;
        }
        public IShopFullInfo GetById(Guid id)
        {
            var result = _repository.Find(id);
            return result.ToShopFullInfoDto();
        }
        public IEnumerable<IShopPositionDto> GetShopsByCityId(Guid cityId)
        {
            var city = _cityRepository.Find(cityId);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }
            var result = _repository.AsQuery().Where(p => p.ShopAddress.CityId == cityId).ToList();
            return result.Select(p => p.ToShopPositionDto());
        }
        public IEnumerable<IShopPositionWithDistanceDto> GetShopsInCustomerAddressArea(Guid userId, Guid customerAddressId)
        {
            var customer = _customerRepository.AsQuery().SingleOrDefault(item => item.UserId == userId);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            var customerAddress = customer.CustomerAddresses.SingleOrDefault(item => item.Id == customerAddressId);
            if (customerAddress == null)
            {
                throw new DomainException("آدرس مشتری یافت نشد");
            }

            var shops = _repository.AsQuery()
                .Where(item =>
                    item.ShopStatus == ShopStatus.Accept && item.IsActive &&
                    item.ShopAddress.Geography.Distance(customerAddress.Geography) <= item.AreaRadius)
                .OrderBy(item => item.ShopAddress.Geography.Distance(customerAddress.Geography)).ToList().Select(item =>
                    new ShopPositionWithDistanceDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Distance = item.ShopAddress.Geography.Distance(customerAddress.Geography),
                        Position = new PositionDto(item.ShopAddress.Geography.ToPosition().Latitude, item.ShopAddress.Geography.ToPosition().Longitude)
                    });
            return shops;
        }
        public IQueryable<IShopFactorSaleDto> GetShopByMarketerId(long marketerId, DateTime fromDate, DateTime toDate)
        {
            var shops = _repository.AsQuery().Where(item => item.MarketerId == marketerId);
            var shopOrderLogs = _shopOrderLogRepository.AsQuery()
                .Where(item => item.CreationTime >= fromDate && item.CreationTime <= toDate);
            var join = shops.GroupJoin(shopOrderLogs, s => s.Id, od => od.Shop.Id,
                (sp, od) => new
                {
                    Shop = sp,
                    orderLogs = od
                }).SelectMany(s => s.orderLogs.DefaultIfEmpty(), (s, f) => new
                {
                    s.Shop,
                    OrderCount = s.orderLogs.Count(),
                    OrderSugesstionCount = s.orderLogs.Count(item => item.HasSuggestions),
                    FactorCount = s.orderLogs.Count(item => item.HasFactor),
                    FactorSum = (decimal?)s.orderLogs
                        .Where(item => item.HasFactor && item.Factor.FactorState == FactorState.Paid)
                        .Sum(item => item.Factor.Price)
                }).GroupBy(p => p.Shop.Id)
                .Select(p => new
                {
                    p.FirstOrDefault().Shop,
                    p.FirstOrDefault().OrderCount,
                    p.FirstOrDefault().OrderSugesstionCount,
                    p.FirstOrDefault().FactorCount,
                    p.FirstOrDefault().FactorSum
                });

            var result = join.Select(item => new ShopFactorSaleDto
            {
                creationTime = item.Shop.CreationTime,
                id = item.Shop.Id,
                personNumber = item.Shop.PersonNumber,
                name = item.Shop.Name,
                pastDateCount = DbFunctions.DiffDays(item.Shop.CreationTime, DateTime.Now),
                factorCount = item.FactorCount,
                factorSum = item.FactorSum,
                orderCount = item.OrderCount,
                orderSugesstionCount = item.OrderSugesstionCount
            });
            return result;
        }
        public IEnumerable<IShopFactorChart> GetShopDailyCharts(Guid shopId)
        {
            var date = DateTime.Now;
            var startDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var factors = _factorRepository.AsQuery()
                .Where(p => p.CreationTime >= startDate && p.Shop.Id == shopId && p.FactorState == FactorState.Paid).ToList();
            var shopFactorDailyCharts = new List<ShopFactorChart>();
            var label = 1;
            foreach (var day in EachHours(startDate, date))
            {
                var endDate = day.AddHours(1);
                var shopFactorDailyChart = new ShopFactorChart
                {
                    Label = Convert.ToString(label),
                    TotalCount = factors.Count(p => p.CreationTime >= day && p.CreationTime <= endDate),
                    TotalSum = factors.Where(p => p.CreationTime >= day && p.CreationTime <= endDate)
                        .Sum(p => p.DiscountPrice)
                };
                shopFactorDailyCharts.Add(shopFactorDailyChart);
                label++;

            }

            return shopFactorDailyCharts;
        }
        public IEnumerable<DateTime> EachHours(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day <= thru; day = day.AddHours(1))
                yield return day;
        }
        public IEnumerable<IShopFactorChart> GetShopWeeklyCharts(Guid shopId)
        {
            var date = DateTime.Now;
            var startDate = date.AddDays(-7);
            var factors = _factorRepository.AsQuery()
                .Where(p => p.CreationTime >= startDate && p.Shop.Id == shopId && p.FactorState == FactorState.Paid).ToList();
            var shopFactorDailyCharts = new List<ShopFactorChart>();

            foreach (var day in EachDay(startDate, date))
            {
                var endDate = day.AddDays(1);
                var shopFactorDailyChart = new ShopFactorChart
                {
                    Label = day.ToFa("m"),
                    TotalCount = factors.Count(p => p.CreationTime >= day && p.CreationTime <= endDate),
                    TotalSum = factors.Where(p => p.CreationTime >= day && p.CreationTime <= endDate)
                        .Sum(p => p.DiscountPrice)
                };
                shopFactorDailyCharts.Add(shopFactorDailyChart);

            }
            return shopFactorDailyCharts;
        }
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day <= thru; day = day.AddDays(1))
                yield return day;
        }
        public IEnumerable<IShopFactorChart> GetShopMonthlyCharts(Guid shopId)
        {
            var date = DateTime.Now;
            var startDate = date.AddDays(-30);

            var shopFactorDailyCharts = new List<ShopFactorChart>();
            foreach (var day in EachDay(startDate, date))
            {
                var endDate = day.AddDays(1);

                var shopFactorDailyChart = new ShopFactorChart
                {
                    Label = day.ToFa("m"),
                    TotalCount = _factorRepository.AsQuery().Count(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.Id == shopId && p.FactorState == FactorState.Paid),
                    TotalSum = _factorRepository.AsQuery().Where(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.Id == shopId && p.FactorState == FactorState.Paid).ToList()
                        .Sum(p => p.DiscountPrice)
                };
                shopFactorDailyCharts.Add(shopFactorDailyChart);
            }
            return shopFactorDailyCharts;
        }
        public IEnumerable<IShopFactorChart> GetShopYearlyCharts(Guid shopId)
        {
            var date = DateTime.Now;
            var startDate = date.AddMonths(-(date.Month - 1)).AddDays(-(date.Day - 1));
            var shopFactorDailyCharts = new List<ShopFactorChart>();

            foreach (var day in EachYear(startDate, date))
            {
                var endDate = day.AddMonths(1);
                var shopFactorDailyChart = new ShopFactorChart
                {
                    Label = day.ToFa("y"),
                    TotalCount = _factorRepository.AsQuery().Count(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.Id == shopId && p.FactorState == FactorState.Paid),
                    TotalSum = _factorRepository.AsQuery().Where(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.Id == shopId && p.FactorState == FactorState.Paid).ToList()
                        .Sum(p => p.DiscountPrice)
                };
                shopFactorDailyCharts.Add(shopFactorDailyChart);

            }
            return shopFactorDailyCharts;
        }
        public IQueryable<IShopDto> IsHaveSellingShop(int month, long marketerId)
        {
            if (month <= 0)
            {
                throw new DomainException("عدد وارد شده نا معتبر است");
            }
            var date = DateTime.Today.AddMonths(-month);
            var shops = _repository.AsQuery().Where(p => p.MarketerId == marketerId);
            var factors = _factorRepository.AsQuery().Where(p => p.CreationTime >= date);
            var shopFactor = shops.Join(factors, s => s.Id, f => f.Shop.Id, (shop, factos) => new
            {
                Factor = factos,
                Shop = shop
            });

            var isSellingShop = shopFactor.Select(item => new ShopDto
            {
                Id = item.Shop.Id,
                FirstName = item.Shop.FirstName,
                LastName = item.Shop.LastName,
                EmailAddress = item.Shop.LastName,
                IsActive = item.Shop.IsActive,
                UserId = item.Shop.UserId,
                MobileNumber = item.Shop.MobileNumber,
                PersonNumber = item.Shop.PersonNumber,
                Name = item.Shop.Name,
                ShopStatus = item.Shop.ShopStatus,
                DescriptionStatus = item.Shop.DescriptionStatus,
                Description = item.Shop.Description,
                NationalCode = item.Shop.NationalCode,
                DefaultDiscount = item.Shop.DefaultDiscount,
                CreationTime = item.Shop.CreationTime,
                HasMarketer = item.Shop.MarketerId == null,
                MarketerId = item.Shop.MarketerId
            });
            var result = isSellingShop.GroupBy(p => p.Id).Select(e => e.FirstOrDefault());
            return result;
        }
        public IShopWithLogDto GetLogChangesById(Guid id)
        {
            var shop = _repository.AsQuery().SingleOrDefault(p => p.Id == id);
            return shop.ToShopLogDto();

        }
        public async Task<IShopCustomerSubsetReportDto> GetShopCustomerSubsetReport(Guid shopId)
        {
            var appSetting = _applicationSettingRepository.AsQuery().FirstOrDefault();
            var shop = await _repository.FindAsync(shopId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var countShopCustomerSubsetNotSettlement =
                shop.CustomerSubsets.Count(p => !p.IsSettlement && !p.HavePaidFactor);
            var countShopCustomerSubsetHaveFactorPaidSumAmount =
                shop.CustomerSubsets.Count(p => !p.IsSettlement && p.HavePaidFactor);

            var result = new ShopCustomerSubsetReportDto
            {
                Count = shop.CustomerSubsets.Count,
                PaidFactorCount = shop.CustomerSubsets.Count(p => p.HavePaidFactor),
                SettlementCount = shop.CustomerSubsets.Count(p => p.IsSettlement),
                NotSettlementCount = shop.CustomerSubsets.Count(p => !p.IsSettlement),
                ShopCustomerSubsetHaveFactorPaidSumAmount = countShopCustomerSubsetHaveFactorPaidSumAmount * appSetting.ShopCustomerSubsetHaveFactorPaidAmount,
                ShopCustomerSubsetSumAmount = countShopCustomerSubsetNotSettlement * appSetting.ShopCustomerSubsetAmount
            };
            return result;
        }
        public IQueryable<IShopsCustomerSubsetReportDto> GetShopsCustomerSubsetReport(DateTime fromDate, DateTime toDate)
        {
            var shops = _repository.AsQuery();
            var marketers = _marketerRepository.AsQuery();
            var shopMarketers = shops.Join(marketers, s => s.MarketerId, m => m.Id,
                    (shop, marketer) => new
                    {
                        Shop = shop,
                        Marketer = marketer
                    }).Where(p =>
                    p.Shop.CustomerSubsets.Any(cs => cs.CreationTime >= fromDate && cs.CreationTime <= toDate))
                .Select(item => new ShopsCustomerSubsetReportDto
                {
                    MobileNumber = item.Shop.MobileNumber,
                    FirstName = item.Shop.FirstName,
                    LastName = item.Shop.LastName,
                    PersonNumber = item.Shop.PersonNumber,
                    Name = item.Shop.Name,
                    MarketerFullName = item.Marketer.FirstName + item.Marketer.LastName,
                    CustomerSubsetsCount = item.Shop.CustomerSubsets.Count,
                    CustomerSubsetsHavePaidFactorCount = item.Shop.CustomerSubsets
                        .Count(p => p.HavePaidFactor && p.PaidFactorDate >= fromDate && p.PaidFactorDate <= toDate),
                    CustomerSubsetsNotSettlementCount = item.Shop.CustomerSubsets.Count(p => !p.IsSettlement),
                    CustomerSubsetsSettlementCount = item.Shop.CustomerSubsets.Count(p => p.IsSettlement)
                });
            return shopMarketers;
        }
        public IEnumerable<DateTime> EachYear(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day <= thru; day = day.AddMonths(1))
                yield return day;
        }
    }
}