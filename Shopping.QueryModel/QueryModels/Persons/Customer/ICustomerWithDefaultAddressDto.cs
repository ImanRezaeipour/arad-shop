namespace Shopping.QueryModel.QueryModels.Persons.Customer
{
    public interface ICustomerWithDefaultAddressDto : ICustomerDto
    {
        IDefultCustomerAddressDto DefultCustomerAddress { get; set; }
    }
}