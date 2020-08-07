using System;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates
{
    public class ApplicationSetting : AggregateRoot<Guid>
    {
        protected ApplicationSetting() { }
        public ApplicationSetting(Guid id, decimal minimumDiscount,
            decimal maximumDiscount, int maximumDeliveryTime,
            int orderSuggestionExpireTime, decimal minimumBuy,
            string shopAppVersion, string shopDownloadLink, string customerAppVersion,
            string customerDownloadLink, string shopAppVersionIos, string shopDownloadLinkIos,
            string customerAppVersionIos, string customerDownloadLinkIos, int factorExpireTime,
            bool iosStoreCheck, decimal shopCustomerSubsetAmount, decimal shopCustomerSubsetHaveFactorPaidAmount,
            int shopCustomerSubsetHaveFactorPaidCount,
            bool recommendedSystemIsActive, int customerRequestOrderDuration, int customerRequestOrderCount,
            int orderItemResponseTime, int orderExpireOpenTime)
        {
            Id = id;
            OrderItemResponseTime = orderItemResponseTime;
            OrderExpireOpenTime = orderExpireOpenTime;
            CustomerRequestOrderCount = customerRequestOrderCount;
            CustomerRequestOrderDuration = customerRequestOrderDuration;
            OrderSuggestionExpireTime = orderSuggestionExpireTime;
            MinimumDiscount = minimumDiscount;
            FactorExpireTime = factorExpireTime;
            MaximumDiscount = maximumDiscount;
            MaximumDeliveryTime = maximumDeliveryTime;
            MinimumBuy = minimumBuy;
            ShopAppVersion = shopAppVersion;
            ShopDownloadLink = shopDownloadLink;
            CustomerAppVersion = customerAppVersion;
            CustomerDownloadLink = customerDownloadLink;
            ShopAppVersionIos = shopAppVersionIos;
            ShopDownloadLinkIos = shopDownloadLinkIos;
            CustomerAppVersionIos = customerAppVersionIos;
            CustomerDownloadLinkIos = customerDownloadLinkIos;
            IosStoreCheck = iosStoreCheck;
            ShopCustomerSubsetAmount = shopCustomerSubsetAmount;
            ShopCustomerSubsetHaveFactorPaidAmount = shopCustomerSubsetHaveFactorPaidAmount;
            ShopCustomerSubsetHaveFactorPaidCount = shopCustomerSubsetHaveFactorPaidCount;
            RecommendedSystemIsActive = recommendedSystemIsActive;
        }
        public int CustomerRequestOrderCount { get; set; }
        /// <summary>
        /// ثانبیه
        /// </summary>
        public int CustomerRequestOrderDuration { get; set; }
        public decimal MinimumDiscount { get; set; }
        public decimal MaximumDiscount { get; set; }
        public int FactorExpireTime { get; set; }
        public int MaximumDeliveryTime { get; set; }
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
        public bool RecommendedSystemIsActive { get; set; }
        /// <summary>
        /// x time  -- مدت زمان بازکردن درخواست توسط فروشگاه  (ثانیه
        /// </summary>
        public int OrderExpireOpenTime { get; set; }
        /// <summary>
        /// y time   --  مهلت اعمال تغییر و ارسال پیشنهاد فروشگاه (ثانبه
        /// </summary>
        public int OrderItemResponseTime { get; set; }
        public override void Validate()
        {
        }
    }
}