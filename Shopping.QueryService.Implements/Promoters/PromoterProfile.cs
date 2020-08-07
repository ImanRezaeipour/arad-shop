using System.Linq;
using AutoMapper;
using Shopping.DomainModel.Aggregates.Promoters.Aggregates;
using Shopping.QueryModel.QueryModels.Promoters;

namespace Shopping.QueryService.Implements.Promoters
{
    public class PromoterProfile : Profile
    {
        public PromoterProfile()
        {
            CreateMap<Promoter, IPromoterDto>().ForMember(dest => dest.CustomerSubsetHavePaidFactorCount,
                opt => opt.MapFrom(src => src.CustomerSubsets.Count(p => p.HavePaidFactor)));
        }
    }
}