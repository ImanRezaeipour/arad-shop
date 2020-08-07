using System;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.ShipmentState
{
    public class ReverseShipmentState: ShipmentStateBase
    {
        protected ReverseShipmentState()
        {
        }
        public ReverseShipmentState(Guid id, string reason) : base(id)
        {
            Reason = reason;
        }
        public string Reason { get;private set; }
        public override void Validate()
        {
        }
    }
}