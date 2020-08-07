using System;

namespace Shopping.QueryModel.QueryModels.Persons.Customer
{
    public interface IDefultCustomerAddressDto
    {
        Guid Id { get; set; }
        string Title { get; set; }
        string AddressText { get; set; }
        string PhoneNumber { get; set; }
        string CityName { get; set; }
        IPositionDto Position { get; set; }
    }
}