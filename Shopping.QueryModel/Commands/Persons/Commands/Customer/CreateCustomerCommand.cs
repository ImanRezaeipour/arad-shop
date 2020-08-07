using Shopping.Commands.Commands.Persons.Commands.Abstract;

namespace Shopping.Commands.Commands.Persons.Commands.Customer
{
    public class CreateCustomerCommand : PersonCommand
    {
        public DefultCustomerAddressCommand CustomerAddress { get; set; }
    }
}