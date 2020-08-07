using System;

namespace Shopping.DomainModel.Aggregates.ApplicationSettings.Interfaces
{
    public interface IApplicationSettingDomainService
    {
        void CheckDiscount(decimal minimumDiscount, decimal maximumDiscount);
        void CheckDiscountInRangeSetting(int discount);
        void CheckShippingTime(int shippingTime);
        void CheckMinimumBuy(decimal price);
        void CheckOrderExpireTime(DateTime orderTime);
    }
}