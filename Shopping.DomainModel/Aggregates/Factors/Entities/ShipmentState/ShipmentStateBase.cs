using System;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.ShipmentState
{
    public abstract class ShipmentStateBase : Entity<Guid>, IHasCreationTime
    {
        protected ShipmentStateBase()
        {
            
        }
        protected ShipmentStateBase(Guid id)
        {
            Id = id;
            CreationTime = DateTime.Now;
        }
        public DateTime CreationTime { get; private set; }
    }
}