namespace Shopping.QueryModel.QueryModels.ApplicationSettings
{
    public interface IApplicationSettingDto
    {
        decimal MinimumDiscount { get; set; }
        decimal MaximumDiscount { get; set; }
        int OrderSuggestionExpireTime { get; set; }
        int FactorExpireTime { get; set; }
        int MaximumDeliveryTime { get; set; }
        decimal MinimumBuy { get; set; }
        string ShopAppVersion { get; set; }
        string ShopDownloadLink { get; set; }
        string CustomerAppVersion { get; set; }
        string CustomerDownloadLink { get; set; }
        string ShopAppVersionIos { get; set; }
        string ShopDownloadLinkIos { get; set; }
        string CustomerAppVersionIos { get; set; }
        string CustomerDownloadLinkIos { get; set; }
        bool IosStoreCheck { get; set; }
        decimal ShopCustomerSubsetAmount { get; set; }
        decimal ShopCustomerSubsetHaveFactorPaidAmount { get; set; }
        int ShopCustomerSubsetHaveFactorPaidCount { get; set; }
        bool RecommendedSystemIsActive { get; set; }
        int CustomerRequestOrderCount { get; set; }
        int CustomerRequestOrderDuration { get; set; }
        /// <summary>
        /// x time  -- مدت زمان بازکردن درخواست توسط فروشگاه
        /// </summary>
        int OrderExpireOpenTime { get; set; }
        /// <summary>
        /// t time   --  مهلت اعمال تغییر و ارسال پیشنهاد فروشگاه
        /// </summary>
        int OrderItemResponseTime { get; set; }

        /// <summary>
        /// این فیلد پاک نشه چون ios میترکه
        /// </summary>
        int OrderExpierTime { get; set; }
    }
}