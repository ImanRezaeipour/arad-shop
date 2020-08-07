using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.FakeIos.Customers.Responses
{
    public class LoginFakeCustomerIosCommandResponse : ShoppingCommandResponseBase
    {
        public LoginFakeCustomerIosCommandResponse(Guid id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}