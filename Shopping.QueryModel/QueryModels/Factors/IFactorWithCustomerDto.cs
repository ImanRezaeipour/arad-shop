using Shopping.QueryModel.QueryModels.Persons.Customer;

namespace Shopping.QueryModel.QueryModels.Factors
{
    public interface IFactorWithCustomerDto : IFactorDto
    {
        ICustomerDto Customer { get; set; }
        IFactorAddressDto FactorAddress { get; set; }
    }
}