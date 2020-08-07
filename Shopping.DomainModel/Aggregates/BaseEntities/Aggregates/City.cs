using System;
using System.Collections.Generic;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.BaseEntities.Aggregates
{
    public class City : AggregateRoot
    {
        protected City()
        {

        }
        public City(Guid id, string code, string cityName, Province province)
        {
            Id = id;
            Code = code;
            CityName = cityName;
            Province = province;
            IsActive = true;
        }
        public string CityName { get; set; }
        public string Code { get; set; }
        public virtual Province Province { get; set; }
        public bool IsActive { get; private set; }
        public void Active() => IsActive = true;
        public void DeActive() => IsActive = false;
        public virtual ICollection<Zone> Zones { get; set; }
        public override void Validate()
        {
        }
    }
}