using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements.Discounts;
using Shopping.QueryModel.QueryModels.Discounts;

namespace Shopping.QueryService.Interfaces.Discounts
{
    public interface IPercentDiscountQueryService
    {
        Task<IEnumerable<IProductDiscountDto>> GetProductPercentDiscountById(Guid percentDiscountId);
        Task<MobilePagingResultDto<IProductDiscountDto>> GetProductPercentDiscountByIdPaging(Guid percentDiscountId,
            PagedInputDto pagedInput);
        IPercentDiscountDto GetById(Guid percentDiscountId);
        IQueryable<DiscountBaseDto> GetAll();
        IQueryable<DiscountPercentShopSellDto> GetPercentDiscountShopSells(Guid discountId);
        PercentDiscountSumReportDto SumOfReport(Guid discountId);
        bool CheckUserTodayHavePercentDiscount(Guid userId);
    }
}