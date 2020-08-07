using System;

namespace Shopping.QueryModel.QueryModels.Persons.Customer
{
    public interface ICustomerAddressDto
    {
        Guid Id { get; set; }
        string Title { get; set; }
        Guid ProvinceId { get; set; }
        Guid CityId { get; set; }
        string AddressText { get; set; }
        string PhoneNumber { get; set; }
        string CityName { get; set; }
        IPositionDto Position { get; set; }
        bool IsDefault { get; set; }
    }
}