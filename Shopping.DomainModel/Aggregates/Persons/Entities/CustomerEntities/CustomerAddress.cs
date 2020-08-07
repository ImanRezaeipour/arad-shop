using System;
using System.Data.Entity.Spatial;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.Persons.Entities.CustomerEntities
{
    public class CustomerAddress : Entity<Guid>
    {
        protected CustomerAddress() { }
        public CustomerAddress(Guid id, string title, string addressTest, string phoneNumber,
            Guid cityId, string cityCode, string cityName, DbGeography geography,
            Guid provinceId, string provinceName, string provinceCode, bool isDefault)
        {
            Id = id;
            Title = title;
            CityId = cityId;
            CityName = cityName;
            CityCode = cityCode;
            Geography = geography;
            IsDefault = isDefault;
            ProvinceId = provinceId;
            AddressText = addressTest;
            PhoneNumber = phoneNumber;
            ProvinceCode = provinceCode;
            ProvinceName = provinceName;
        }
        public string Title { get; set; }
        public Guid ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceCode { get; set; }
        public string AddressText { get; set; }
        public string PhoneNumber { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public DbGeography Geography { get; set; }
        public bool IsDefault { get; set; }
        public override void Validate()
        {
        }
    }
}