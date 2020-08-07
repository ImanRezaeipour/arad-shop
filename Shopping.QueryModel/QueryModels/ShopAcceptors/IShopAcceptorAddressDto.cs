using System;

namespace Shopping.QueryModel.QueryModels.ShopAcceptors
{
    public interface IShopAcceptorAddressDto
    {
        string AddressText { get;  set; }
        Guid CityId { get;  set; }
        string CityName { get;  set; }
    }
}