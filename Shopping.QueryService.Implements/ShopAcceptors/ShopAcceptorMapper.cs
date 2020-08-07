using AutoMapper;
using Shopping.DomainModel.Aggregates.ShopAcceptors.Aggregates;
using Shopping.QueryModel.QueryModels.ShopAcceptors;

namespace Shopping.QueryService.Implements.ShopAcceptors
{
    public static class ShopAcceptorMapper
    {
        public static IShopAcceptorsWithAddressDto ToDto(this ShopAcceptor src)
        {
            return Mapper.Map<IShopAcceptorsWithAddressDto>(src);
        }
    }
}