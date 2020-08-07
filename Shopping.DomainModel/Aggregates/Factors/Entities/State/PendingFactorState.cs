using System;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.State
{
    public class PendingFactorState : FactorStateBase
    {
        protected PendingFactorState()
        {
        }

        public PendingFactorState(Guid id) : base(id)
        {
        }

        public override void Validate()
        {
        }
    }
}