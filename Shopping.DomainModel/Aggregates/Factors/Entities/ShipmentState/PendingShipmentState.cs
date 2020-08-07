using System;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.ShipmentState
{
    public class PendingShipmentState: ShipmentStateBase
    {
        protected PendingShipmentState()
        {
        }
        public PendingShipmentState(Guid id) : base(id)
        {
        }
        public override void Validate()
        {
        }
    }
}