using System.Collections.Generic;

namespace Shopping.QueryModel.QueryModels.Persons.Customer
{
    public interface ICustomerWithAddressesDto : ICustomerDto
    {
        IList<ICustomerAddressDto> CustomerAddresses { get; set; }
    }
}