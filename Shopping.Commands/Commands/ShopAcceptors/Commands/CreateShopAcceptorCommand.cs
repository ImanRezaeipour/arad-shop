using Shopping.Commands.Commands.Shared;
using Shopping.Commands.Commands.ShopAcceptors.Commands.CoomandItems;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.ShopAcceptors.Commands
{
    public class CreateShopAcceptorCommand:ShoppingCommandBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string ShopName { get; set; }
        public ShopAcceptorAddressCommand ShopAcceptorAddress { get; set; }
        public UserInfoCommandItem UserInfo { get; set; }
    }
}