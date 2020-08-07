using System;
using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.ShopAcceptors.ValueObjects
{
    public class ShopAcceptorAddress : ValueObject<ShopAcceptorAddress>
    {
        protected ShopAcceptorAddress()
        {}
        public ShopAcceptorAddress(string addressTest, Guid cityId, string cityName)
        {
            AddressText = addressTest;
            
            CityId = cityId;
            CityName = cityName;
        }
        public string AddressText { get; private set; }
        public Guid CityId { get; private set; }
        public string CityName { get; private set; }

    }
}