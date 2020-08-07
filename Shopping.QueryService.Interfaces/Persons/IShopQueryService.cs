using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.QueryModel.Implements.Persons;
using Shopping.QueryModel.QueryModels.Persons.Shop;
using Shopping.QueryModel.QueryModels.Persons.Shop.Charts;
using Shopping.QueryModel.QueryModels.Persons.Shop.CustomerSubsets;

namespace Shopping.QueryService.Interfaces.Persons
{
    public interface IShopQueryService
    {
        IShopFullInfo GetByUserId(Guid userId);
        IQueryable<ShopDto> GetAll();
        IShopFullInfo GetById(Guid id);
        IEnumerable<IShopPositionDto> GetShopsByCityId(Guid cityId);
        IEnumerable<IShopPositionWithDistanceDto> GetShopsInCustomerAddressArea(Guid userId, Guid customerAddressId);
        IQueryable<IShopFactorSaleDto> GetShopByMarketerId(long marketerId, DateTime fromDate, DateTime toDate);
        IEnumerable<IShopFactorChart> GetShopDailyCharts(Guid shopId);
        IEnumerable<IShopFactorChart> GetShopWeeklyCharts(Guid shopId);
        IEnumerable<IShopFactorChart> GetShopMonthlyCharts(Guid shopId);
        IEnumerable<IShopFactorChart> GetShopYearlyCharts(Guid shopId);
        IQueryable<IShopDto> IsHaveSellingShop(int month, long marketerId);
        IShopWithLogDto GetLogChangesById(Guid id);
        Task<IShopCustomerSubsetReportDto> GetShopCustomerSubsetReport(Guid shopId);
        IQueryable<IShopsCustomerSubsetReportDto> GetShopsCustomerSubsetReport(DateTime fromDate,DateTime toDate);
    }
}