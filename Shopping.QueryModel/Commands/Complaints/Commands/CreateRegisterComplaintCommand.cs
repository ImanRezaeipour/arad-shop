using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Complaints.Commands
{
    public class CreateRegisterComplaintCommand:ShoppingCommandBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ShopId { get; set; }
       
    }
}