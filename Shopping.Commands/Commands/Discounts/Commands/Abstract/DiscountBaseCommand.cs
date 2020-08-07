using Shopping.Commands.Commands.Shared;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Discounts.Commands.Abstract
{
    public class DiscountBaseCommand: ShoppingCommandBase
    {
        public string Description { get; set; }
        public UserInfoCommandItem UserInfoCommand { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Title { get; set; }
    }
}