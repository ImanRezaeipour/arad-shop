using Shopping.Infrastructure.Core.Domain.Values;
using System;
using System.Data.Entity.Spatial;

namespace Shopping.DomainModel.Aggregates.Persons.ValueObjects
{
    public class DefultCustomerAddress : ValueObject<DefultCustomerAddress>
    {
        protected DefultCustomerAddress() { }
        public DefultCustomerAddress(Guid? customerAddressId, string title, string addressTest, string phoneNumber,
            Guid? cityId, string cityCode, string cityName, DbGeography geography,
            Guid? provinceId, string provinceName, string provinceCode)
        {
            CustomerAddressId = customerAddressId;
            AddressText = addressTest;
            PhoneNumber = phoneNumber;
            CityId = cityId;
            CityName = cityName;
            Geography = geography;
            CityCode = cityCode;
            ProvinceCode = provinceCode;
            ProvinceName = provinceName;
            ProvinceId = provinceId;
            Title = title;
        }

        public string Title { get; private set; }
        public Guid? ProvinceId { get; private set; }
        public string ProvinceName { get; private set; }
        public string ProvinceCode { get; private set; }
        public Guid? CustomerAddressId { get; private set; }
        public string AddressText { get; private set; }
        public string PhoneNumber { get; private set; }
        public Guid? CityId { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public DbGeography Geography { get; private set; }
        public static DefultCustomerAddress CreateNull()
        {
            return new DefultCustomerAddress(null, "", "", "", null, "", "", null, null, "", "");
        }
        public bool HasValue => (ProvinceId != null || !string.IsNullOrEmpty(ProvinceName) ||
                                 !string.IsNullOrEmpty(ProvinceCode) || !string.IsNullOrEmpty(AddressText)
                                 || !string.IsNullOrEmpty(PhoneNumber) || !string.IsNullOrEmpty(CityName) ||
                                 !string.IsNullOrEmpty(CityCode) || Geography != null);
    }
}