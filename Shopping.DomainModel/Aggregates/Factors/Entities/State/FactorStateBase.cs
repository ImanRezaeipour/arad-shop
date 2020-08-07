using System;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.State
{
    public abstract class FactorStateBase : Entity
    {
        protected FactorStateBase() { }
        protected FactorStateBase(Guid id)
        {
            Id = id;
        }
    }
}