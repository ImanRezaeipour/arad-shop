using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.QueryModel.QueryModels.Marketers;
using Shopping.QueryModel.QueryModels.Marketers.Charts;

namespace Shopping.QueryService.Interfaces.Marketers
{
    public interface IMarketerQueryService
    {
        IQueryable<IMarketerDto> GetAll();
        Task<IMarketerDto> GetMarketerByBarcodeId(Guid barcodeId);
        Task<IMarketerFullInfoDto> Get(long id);
        Task<IList<IMarketerCommentDto>> GetMarketerComments(long id);
        IEnumerable<IMarketerShopFactorChart> GetMarketerShopDailyCharts(long markterId);
        IEnumerable<IMarketerShopFactorChart> GetMarketerShopWeeklyCharts(long markterId);
        IEnumerable<IMarketerShopFactorChart> GetMarketerShopMonthlyCharts(long markterId);
        IEnumerable<IMarketerShopFactorChart> GetMarketerShopYearlyCharts(long markterId);
        decimal GetTotalSalesAmount(long markterId);
    }
}