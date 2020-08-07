using AutoMapper;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.QueryModel.QueryModels.Factors;

namespace Shopping.QueryService.Implements.Factors
{
    public static class FactorMapper
    {
        public static IFactorFullInfoDto ToDto(this Factor src)
        {
            return Mapper.Map<IFactorFullInfoDto>(src);
        }
        public static IFactorDto ToFactorDto(this Factor src)
        {
            return Mapper.Map<IFactorDto>(src);
        }
        public static IFactorWithCustomerDto ToFactorWithCustomerDto(this Factor src)
        {
            return Mapper.Map<IFactorWithCustomerDto>(src);
        }
        public static IFactorWithShopDto ToFactorWithShopDto(this Factor src)
        {
            return Mapper.Map<IFactorWithShopDto>(src);
        }
    }
}