using System;
using System.Data.Entity.Spatial;
using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Persons.ValueObjects
{
    public class ShopAddress : ValueObject<ShopAddress>
    {
        protected ShopAddress() { }
        public ShopAddress(Guid cityId, string cityCode, string cityName, string addressText, string phoneNumber, DbGeography geography, string shopMobileNumber, long zoneId,
            Guid provinceId, string provinceCode, string provinceName)
        {
            ProvinceId = provinceId;
            ProvinceName = provinceName;
            ProvinceCode = provinceCode;
            CityCode = cityCode;
            CityId = cityId;
            CityName = cityName;
            AddressText = addressText;
            PhoneNumber = phoneNumber;
            Geography = geography;
            ShopMobileNumber = shopMobileNumber;
            ZoneId = zoneId;
        }
        public long ZoneId { get; private set; }
        public Guid CityId { get; private set; }
        public string CityCode { get; set; }
        public string CityName { get; private set; }
        public string AddressText { get; private set; }
        public string PhoneNumber { get; private set; }
        public string ShopMobileNumber { get; private set; }
        public DbGeography Geography { get; private set; }
        public Guid ProvinceId { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
    }
}