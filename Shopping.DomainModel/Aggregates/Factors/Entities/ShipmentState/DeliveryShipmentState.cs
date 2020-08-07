using System;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.ShipmentState
{
    public class DeliveryShipmentState: ShipmentStateBase
    {
        protected DeliveryShipmentState()
        {
        }
        public DeliveryShipmentState(Guid id) : base(id)
        {
        }
        public override void Validate()
        {
        }
    }
}