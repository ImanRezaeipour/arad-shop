using System;

namespace Shopping.Infrastructure.Core.Domain.Entities.Interfaces
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; set; }
    }
}