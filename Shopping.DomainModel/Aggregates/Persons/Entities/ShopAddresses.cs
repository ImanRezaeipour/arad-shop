using System;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.Person.Entitise
{
    public class ShopAddresses: Entity
    {
        public string City { get; set; }
        public override void Validate()
        {
            
        }
    }
}