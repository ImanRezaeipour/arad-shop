using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.ShopMarketersHistories.Aggregates;
using Shopping.QueryModel.QueryModels.ShopMarketersHistories;
using Shopping.QueryService.Interfaces.ShopMarketersHistories;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.ShopMarketersHistories
{
    public class ShopMarketersHistoryQueryService : IShopMarketersHistoryQueryService
    {
        private readonly IReadOnlyRepository<ShopMarketersHistory, Guid> _repository;
        public ShopMarketersHistoryQueryService(IReadOnlyRepository<ShopMarketersHistory, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<IList<IShopMarketersHistoryDto>> GetShopMarketers(Guid shopId)
        {
            var result = await _repository.AsQuery().Where(item => item.Shop.Id == shopId)
                .OrderBy(item => item.CreationTime).ToListAsync();
            return result.Select(item => item.ToDto()).ToList();
        }
    }
}