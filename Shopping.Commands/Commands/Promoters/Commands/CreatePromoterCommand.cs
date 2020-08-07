using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Promoters.Commands
{
    public class CreatePromoterCommand : ShoppingCommandBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string MobileNumber { get; set; }
    }
}