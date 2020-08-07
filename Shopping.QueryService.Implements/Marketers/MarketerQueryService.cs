using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PersianDate;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.Implements.Marketers;
using Shopping.QueryModel.QueryModels.Marketers;
using Shopping.QueryModel.QueryModels.Marketers.Charts;
using Shopping.QueryService.Interfaces.Marketers;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Marketers
{
    public class MarketerQueryService : IMarketerQueryService
    {
        private readonly IReadOnlyRepository<Marketer, long> _repository;
        private readonly IReadOnlyRepository<Shop, Guid> _shopRepository;
        private readonly IReadOnlyRepository<Factor, long> _factorRepository;

        public MarketerQueryService(IReadOnlyRepository<Marketer, long> repository, IReadOnlyRepository<Shop, Guid> shopRepository, IReadOnlyRepository<Factor, long> factorRepository)
        {
            _repository = repository;
            _shopRepository = shopRepository;
            _factorRepository = factorRepository;
        }
        public IQueryable<IMarketerDto> GetAll()
        {
            var marketers = _repository.AsQuery();
            var shops = _shopRepository.AsQuery().Where(item => item.IsActive && item.ShopStatus == ShopStatus.Accept);

            var join = marketers.GroupJoin(shops, m => m.Id, s => s.MarketerId,
                    (mar, sp) => new
                    {
                        Marketer = mar,
                        Shop = sp
                    }).SelectMany(x => x.Shop.DefaultIfEmpty(), (m, s) => new
                    {
                        m.Marketer,
                        ShopCount = m.Shop.Count()
                    }).GroupBy(p => p.Marketer.Id)
                .Select(p => new
                {
                    p.FirstOrDefault().Marketer,
                    p.FirstOrDefault().ShopCount
                });

            var result = join.Select(item => new MarketerDto
            {
                CreationTime = item.Marketer.CreationTime,
                Id = item.Marketer.Id,
                IsActive = item.Marketer.IsActive,
                FirstName = item.Marketer.FirstName,
                LastName = item.Marketer.LastName,
                MaxMarketerAllowed = item.Marketer.MaxMarketerAllowed,
                SubsetShopCount = item.ShopCount,
                Image = item.Marketer.Image,
                BarcodeId = item.Marketer.BarcodeId
            });
            return result;
        }
        public async Task<IMarketerDto> GetMarketerByBarcodeId(Guid barcodeId)
        {
            var marketer = await _repository.AsQuery().SingleOrDefaultAsync(p => p.BarcodeId == barcodeId);
            if (marketer == null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            return marketer.ToDto();
        }
        public async Task<IMarketerFullInfoDto> Get(long id)
        {
            var marketer = await _repository.FindAsync(id);
            return marketer.ToFullInfoDto();
        }

        public async Task<IList<IMarketerCommentDto>> GetMarketerComments(long id)
        {
            var marketer = await _repository.FindAsync(id);
            if (marketer == null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            var result = marketer.Comments.ToList()
                .Select(item => item.ToDto()).ToList();
            return result;
        }

        public IEnumerable<IMarketerShopFactorChart> GetMarketerShopDailyCharts(long marketerId)
        {
            var date = DateTime.Now;
            var startDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var shopFactorDailyCharts = new List<MarketerShopFactorChart>();
            var label = 1;
            foreach (var day in EachHours(startDate, date))
            {
                var endDate = day.AddHours(1);
                var shopFactorDailyChart = new MarketerShopFactorChart
                {
                    Label = Convert.ToString(label),
                    TotalCount = _factorRepository.AsQuery().Count(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.MarketerId == marketerId && p.FactorState == FactorState.Paid),
                    TotalSum = _factorRepository.AsQuery().Where(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.MarketerId == marketerId &&p.FactorState==FactorState.Paid).ToList()
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
        public IEnumerable<IMarketerShopFactorChart> GetMarketerShopWeeklyCharts(long marketerId)
        {
            var date = DateTime.Now;
            var startDate = date.AddDays(-7);
            var shopFactorDailyCharts = new List<MarketerShopFactorChart>();

            foreach (var day in EachDay(startDate, date))
            {
                var endDate = day.AddDays(1);
                var shopFactorDailyChart = new MarketerShopFactorChart
                {
                    Label = day.ToFa("m"),
                    TotalCount = _factorRepository.AsQuery().Count(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.MarketerId == marketerId && p.FactorState == FactorState.Paid),
                    TotalSum = _factorRepository.AsQuery().Where(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.MarketerId == marketerId && p.FactorState == FactorState.Paid).ToList()
                        .Sum(p => p.DiscountPrice)
                };
                shopFactorDailyCharts.Add(shopFactorDailyChart);

            }
            return shopFactorDailyCharts;
        }

        public IEnumerable<IMarketerShopFactorChart> GetMarketerShopMonthlyCharts(long marketerId)
        {
            var date = DateTime.Now;
            var startDate = date.AddDays(-30);
            var shopFactorDailyCharts = new List<MarketerShopFactorChart>();

            foreach (var day in EachDay(startDate, date))
            {
                var endDate = day.AddDays(1);
                var shopFactorDailyChart = new MarketerShopFactorChart
                {
                    Label = day.ToFa("m"),
                    TotalCount = _factorRepository.AsQuery().Count(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.MarketerId == marketerId && p.FactorState == FactorState.Paid),
                    TotalSum = _factorRepository.AsQuery().Where(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.MarketerId == marketerId && p.FactorState == FactorState.Paid).ToList()
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
        public IEnumerable<IMarketerShopFactorChart> GetMarketerShopYearlyCharts(long marketerId)
        {
            var date = DateTime.Now;
            var startDate = date.AddMonths(-(date.Month - 1)).AddDays(-(date.Day - 1));
            var shopFactorDailyCharts = new List<MarketerShopFactorChart>();

            foreach (var day in EachYear(startDate, date))
            {
                var endDate = day.AddMonths(1);
                var shopFactorDailyChart = new MarketerShopFactorChart
                {
                    Label = day.ToFa("y"),
                    TotalCount = _factorRepository.AsQuery()
                          .Count(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.MarketerId == marketerId && p.FactorState == FactorState.Paid),
                    TotalSum = _factorRepository.AsQuery()
                          .Where(p => p.CreationTime >= day && p.CreationTime <= endDate && p.Shop.MarketerId == marketerId && p.FactorState == FactorState.Paid).ToList()
                          .Sum(p => p.DiscountPrice)
                };
                shopFactorDailyCharts.Add(shopFactorDailyChart);
            }
            return shopFactorDailyCharts;
        }

        public decimal GetTotalSalesAmount(long marketerId)
        {
            var totalSum = _factorRepository.AsQuery()
                .Where(p => p.Shop.MarketerId == marketerId && p.FactorState == FactorState.Paid).ToList()
                .Sum(p => p.DiscountPrice);
            return totalSum;
        }

        public IEnumerable<DateTime> EachYear(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day <= thru; day = day.AddMonths(1))
                yield return day;
        }
    }
}