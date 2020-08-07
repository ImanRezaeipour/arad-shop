namespace Shopping.Infrastructure.Core.Domain.Entities.Interfaces
{
    public interface IPassivable
    {
        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        bool IsActive { get; }
    }
}