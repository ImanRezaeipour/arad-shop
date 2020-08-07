using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Settlings.Commands
{
    public class CreateDailysetllingCommand : ShoppingCommandBase
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}