using System;
using Shopping.QueryModel.QueryModels.ShopAcceptors;

namespace Shopping.QueryModel.Implements.ShopAcceptors
{
    public class ShopAcceptorAddressDto: IShopAcceptorAddressDto
    {
        public string AddressText { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; }
    }
}