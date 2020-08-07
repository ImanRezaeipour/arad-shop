using System;
using System.Linq;
using System.Threading.Tasks;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.QueryModels.Factors;

namespace Shopping.QueryService.Interfaces.Factors
{
    public interface IFactorQueryService
    {
        Task<MobilePagingResultDto<IFactorWithCustomerDto>> GetShopFactors(Guid userId, PagedInputDto pagedInput);
        IFactorDto GetLastFactorWithoutComment(Guid userId);
        IQueryable<IFactorDto> GetAll();
        IQueryable<IFactorDto> GetCustomerFactorsByCustomerId(Guid customerId);
        IQueryable<IFactorDto> GetFactorsByShopId(Guid shopId);
        IFactorFullInfoDto GetById(long id);
        MobilePagingResultDto<IFactorWithCustomerDto> GetPendingShopFactors(Guid userId, PagedInputDto pagedInput);
        MobilePagingResultDto<IFactorWithCustomerDto> GetPaidShopFactors(Guid userId, PagedInputDto pagedInput);
        MobilePagingResultDto<IFactorWithShopDto> GetCustomerFactors(Guid userId, PagedInputDto pagedInput);
        MobilePagingResultDto<IFactorWithCustomerDto> GetShopPaidSendFactors(Guid userId, PagedInputDto pagedInput);
        long GetShopPaidSendFactorsCount(Guid userId);
        MobilePagingResultDto<IFactorWithCustomerDto> GetShopPaidNotSendFactors(Guid userId, PagedInputDto pagedInput);
        long GetShopPaidNotSendFactorsCount(Guid userId);
        MobilePagingResultDto<IFactorWithCustomerDto> GetShopPaidDeliveredFactors(Guid userId, PagedInputDto pagedInput);
        IQueryable<IFactorReportFinancialDto> GetFactorReportFinancial(DateTime from, DateTime to);
        IFactorTotalReportFinancialDto GetTotalFactorReportFinancial(DateTime from, DateTime to);

    }
}