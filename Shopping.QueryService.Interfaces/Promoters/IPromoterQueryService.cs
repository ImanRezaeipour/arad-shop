using System;
using System.Linq;
using System.Threading.Tasks;
using Shopping.QueryModel.Implements.Promoters;
using Shopping.QueryModel.QueryModels.Promoters;

namespace Shopping.QueryService.Interfaces.Promoters
{
    public interface IPromoterQueryService
    {
        IQueryable<PromoterDto> GetAll();
        Task<IPromoterDto> GetAsync(Guid id);
    }
}