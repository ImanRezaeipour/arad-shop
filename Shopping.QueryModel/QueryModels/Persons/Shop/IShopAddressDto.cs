using System;

namespace Shopping.QueryModel.QueryModels.Persons.Shop
{
    public interface IShopAddressDto
    {
        Guid CityId { get; set; }
        Guid ProvinceId { get; set; }
        long ZoneId { get; set; }
        string CityName { get; set; }
        string AddressText { get; set; }
        string PhoneNumber { get; set; }
        IPositionDto Position { get; set; }
        string ShopMobileNumber { get; set; }
    }
}