using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.ApplicationSettings.Commands
{
    public class UpdateApplicationSettingCommand : ShoppingCommandBase
    {
        public decimal MinimumDiscount { get; set; }
        public decimal MaximumDiscount { get; set; }
        public int OrderExpierTime { get; set; }
        public int MaximumDeliveryTime { get; set; }
        public int FactorExpireTime { get; set; }
        public decimal MinimumBuy { get; set; }
        public string ShopAppVersion { get; set; }
        public string ShopDownloadLink { get; set; }
        public string CustomerAppVersion { get; set; }
        public string CustomerDownloadLink { get; set; }
    }
}