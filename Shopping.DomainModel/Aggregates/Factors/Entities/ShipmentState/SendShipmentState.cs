using System;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.ShipmentState
{
    public class SendShipmentState : ShipmentStateBase
    {
        protected SendShipmentState()
        {
        }
        public SendShipmentState(Guid id) : base(id)
        {
        }
        public override void Validate()
        {
        }
    }
}