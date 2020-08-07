using System;
using System.Data.Entity.Spatial;
using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Factors.ValueObjects
{
    public class FactorAddress: ValueObject<FactorAddress>
    {
        protected FactorAddress()
        {}

        public FactorAddress(string addressTest, string phoneNumber, Guid cityId, string cityName, DbGeography geography)
        {
            AddressText = addressTest;
            PhoneNumber = phoneNumber;
            CityId = cityId;
            CityName = cityName;
            Geography = geography;
        }
        public string AddressText { get; private set; }
        public string PhoneNumber { get; private set; }
        public Guid CityId { get; private set; }
        public string CityName { get; private set; }
        public DbGeography Geography { get; private set; }
    }
}