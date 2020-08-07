using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.FakeIos.Customers.Commands
{
    public class RegisterFakeCustomerIosCommand : ShoppingCommandBase
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}