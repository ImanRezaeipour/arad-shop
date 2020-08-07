using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Interfaces;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.ApplicationSettings.Services
{
    public class ApplicationSettingDomainService : IApplicationSettingDomainService
    {
        private readonly IRepository<ApplicationSetting> _repository;
        public ApplicationSettingDomainService(IRepository<ApplicationSetting> repository)
        {
            _repository = repository;
        }
        public void CheckDiscount(decimal minimumDiscount, decimal maximumDiscount)
        {
            if (minimumDiscount > maximumDiscount)
            {
                throw new DomainException("حداقل تخفیف نمی تواند از حداکثر تخفیف بیشتر باشد");
            }
        }
        public void CheckDiscountInRangeSetting(int discount)
        {
            var setting = _repository.AsQuery().FirstOrDefault();
            if (setting != null && discount < setting.MinimumDiscount) throw new DomainException("تخفیف وارد شده از حداقل تخفیف معتبر کمتر می باشد");
            if (setting != null && discount > setting.MaximumDiscount) throw new DomainException("تخفیف وارد شده از حداکثر تخفیف معتبر بیشتر می باشد");

        }
        public void CheckShippingTime(int shippingTime)
        {
            var setting = _repository.AsQuery().FirstOrDefault();
            if (setting != null && shippingTime > setting.MaximumDeliveryTime) throw new DomainException("حداکثر زمان تحویل سفارش از حداکثر زمان معتبر بیشتر می باشد");

        }
        public void CheckMinimumBuy(decimal price)
        {
            var setting = _repository.AsQuery().FirstOrDefault();
            if (setting != null && price < setting.MinimumBuy) throw new DomainException("حداقل مبلغ سفارش از حداقل مبلغ معتبر کمتر می باشد");

        }
        public void CheckOrderExpireTime(DateTime orderTime)
        {
            var setting = _repository.AsQuery().FirstOrDefault();
            var expireOrderTime = orderTime.AddMinutes((double) setting?.OrderExpireOpenTime);
            if (expireOrderTime < DateTime.Now)
                throw new DomainException("زمان ثبت سفارش از زمان معتبر گذشته است");
        }
    }
}