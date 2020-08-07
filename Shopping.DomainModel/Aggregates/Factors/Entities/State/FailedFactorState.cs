using System;
using Parbad.Core;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.State
{
    public class FailedFactorState : FactorStateBase, IHasCreationTime
    {
        protected FailedFactorState() { }
        public FailedFactorState(Guid id, string referenceId, string transactionId, VerifyResultStatus status, string message) : base(id)
        {
            ReferenceId = referenceId;
            TransactionId = transactionId;
            Status = status;
            Message = message;
            CreationTime = DateTime.Now;
        }
        public DateTime CreationTime { get; private set; }
        public string ReferenceId { get; private set; }
        public string TransactionId { get; private set; }
        public VerifyResultStatus Status { get; private set; }
        public string Message { get; private set; }
        public override void Validate()
        {
        }
    }
}