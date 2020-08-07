using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.FakeIos.Customers.Responses
{
    public class RegisterFakeCustomerIosCommandResponse : ShoppingCommandResponseBase
    {
        public RegisterFakeCustomerIosCommandResponse(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}