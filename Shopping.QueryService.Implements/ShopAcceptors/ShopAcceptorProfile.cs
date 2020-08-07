using AutoMapper;
using Shopping.DomainModel.Aggregates.ShopAcceptors.Aggregates;
using Shopping.DomainModel.Aggregates.ShopAcceptors.ValueObjects;
using Shopping.QueryModel.QueryModels.ShopAcceptors;

namespace Shopping.QueryService.Implements.ShopAcceptors
{
    public class ShopAcceptorProfile:Profile
    {
        public ShopAcceptorProfile()
        {
            CreateMap<ShopAcceptor, IShopAcceptorsDto>();
            CreateMap<ShopAcceptor, IShopAcceptorsWithAddressDto>();
            CreateMap<ShopAcceptorAddress, IShopAcceptorAddressDto>();
        }
    }
}