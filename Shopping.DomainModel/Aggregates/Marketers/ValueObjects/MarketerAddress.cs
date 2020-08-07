using System;
using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Marketers.ValueObjects
{
    /// <summary>
    /// آدرس بازاریاب
    /// </summary>
    public class MarketerAddress:ValueObject<MarketerAddress>
    {
        protected MarketerAddress()
        {
        }
        public MarketerAddress(string addressText, string phoneNumber, string mobileNumber, 
            string requiredPhoneNumber, Guid cityId, string cityName)
        {
            AddressText = addressText;
            PhoneNumber = phoneNumber;
            MobileNumber = mobileNumber;
            RequiredPhoneNumber = requiredPhoneNumber;
            CityId = cityId;
            CityName = cityName;
        }
        /// <summary>
        /// آدرس متنی
        /// </summary>
        public string AddressText { get; private set; }
        /// <summary>
        /// شماره ثابت
        /// </summary>
        public string PhoneNumber { get; private set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        public string MobileNumber { get; private set; }
        /// <summary>
        /// شماره تلفن ضروری
        /// </summary>
        public string RequiredPhoneNumber { get;private set; }
        /// <summary>
        /// شناسه شهر
        /// </summary>
        public Guid CityId { get; private set; }
        /// <summary>
        /// نام شهر
        /// </summary>
        public string CityName { get; private set; }
    }
}