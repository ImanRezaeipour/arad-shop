using System;

namespace Shopping.Infrastructure.Core.Domain.Entities.Interfaces
{
    public interface IHasCreationTime
    {
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        DateTime CreationTime { get; }
    }
}