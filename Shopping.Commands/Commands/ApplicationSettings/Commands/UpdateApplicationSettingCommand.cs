using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.ApplicationSettings.Commands
{
    public class UpdateApplicationSettingCommand : ShoppingCommandBase
    {
        public decimal MinimumDiscount { get; set; }
        public decimal MaximumDiscount { get; set; }
        public int MaximumDeliveryTime { get; set; }
        public int FactorExpireTime { get; set; }
        public int OrderSuggestionExpireTime { get; set; }
        public decimal MinimumBuy { get; set; }
        public string ShopAppVersion { get; set; }
        public string ShopDownloadLink { get; set; }
        public string CustomerAppVersion { get; set; }
        public string CustomerDownloadLink { get; set; }
        public string ShopAppVersionIos { get; set; }
        public string ShopDownloadLinkIos { get; set; }
        public string CustomerAppVersionIos { get; set; }
        public string CustomerDownloadLinkIos { get; set; }
        public bool IosStoreCheck { get; set; }
        public decimal ShopCustomerSubsetAmount { get; set; }
        public decimal ShopCustomerSubsetHaveFactorPaidAmount { get; set; }
        public int ShopCustomerSubsetHaveFactorPaidCount { get; set; }
        public bool  RecommendedSystemIsActive { get; set; }
        public int CustomerRequestOrderCount { get; set; }
        public int CustomerRequestOrderDuration { get; set; }
        /// <summary>
        /// x time  -- مدت زمان بازکردن درخواست توسط فروشگاه
        /// </summary>
        public int OrderExpireOpenTime { get; set; }
        /// <summary>
        /// t time   --  مهلت اعمال تغییر و ارسال پیشنهاد فروشگاه
        /// </summary>
        public int OrderItemResponseTime { get; set; }
    }
}