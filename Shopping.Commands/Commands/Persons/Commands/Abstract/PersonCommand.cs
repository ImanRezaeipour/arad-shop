using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Commands.Abstract
{
    public class PersonCommand : ShoppingCommandBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Guid UserId { get; set; }
        public string MobileNumber { get; set; }
    }
}