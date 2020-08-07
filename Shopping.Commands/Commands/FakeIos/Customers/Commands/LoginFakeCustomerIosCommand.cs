using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.FakeIos.Customers.Commands
{
    public class LoginFakeCustomerIosCommand : ShoppingCommandBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}