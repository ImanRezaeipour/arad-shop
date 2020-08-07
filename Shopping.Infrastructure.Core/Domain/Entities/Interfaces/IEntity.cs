using System;

namespace Shopping.Infrastructure.Core.Domain.Entities.Interfaces
{
    public interface IEntity : IEntity<Guid>
    {
    }
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}