using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Promoters.Commands
{
    public class UpdatePromoterCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string MobileNumber { get; set; }
    }
    public class DeletePromoterCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}