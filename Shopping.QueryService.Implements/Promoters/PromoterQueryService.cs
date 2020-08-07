using System;
using System.Linq;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.Promoters.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.QueryModel.Implements.Promoters;
using Shopping.QueryModel.QueryModels.Promoters;
using Shopping.QueryService.Interfaces.Promoters;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Promoters
{
    public class PromoterQueryService : IPromoterQueryService
    {
        private readonly IReadOnlyRepository<Promoter, Guid> _repository;
        public PromoterQueryService(IReadOnlyRepository<Promoter, Guid> repository)
        {
            _repository = repository;
        }
        public IQueryable<PromoterDto> GetAll()
        {
            var result = _repository.AsQuery().Select(item => new PromoterDto
            {
                Id = item.Id,
                MobileNumber = item.MobileNumber,
                NationalCode = item.NationalCode,
                Code = item.Code,
                LastName = item.LastName,
                FirstName = item.FirstName,
                CustomerSubsetCount = item.CustomerSubsetCount,
                CustomerSubsetHavePaidFactorCount = item.CustomerSubsets.Count(p => p.HavePaidFactor)
            });
            return result;
        }
        public async Task<IPromoterDto> GetAsync(Guid id)
        {
            var promoter = await _repository.FindAsync(id);
            if (promoter == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            return promoter.ToDto();
        }
    }
}