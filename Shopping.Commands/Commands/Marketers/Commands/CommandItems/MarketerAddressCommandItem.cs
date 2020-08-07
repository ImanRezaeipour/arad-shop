using System;

namespace Shopping.Commands.Commands.Marketers.Commands.CommandItems
{
    public class MarketerAddressCommandItem
    {
        public string AddressText { get; set; }
        /// <summary>
        /// شماره ثابت
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// شماره تلفن ضروری
        /// </summary>
        public string RequiredPhoneNumber { get; set; }
        /// <summary>
        /// شناسه شهر
        /// </summary>
        public Guid CityId { get; set; }
        /// <summary>
        /// نام شهر
        /// </summary>
        public string CityName { get; set; }
    }
}