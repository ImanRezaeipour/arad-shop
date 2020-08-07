namespace Shopping.Infrastructure.Core.Domain.Entities.Interfaces
{
    public interface ISoftDelete
    {
        /// <summary>
        /// حذف؟
        /// </summary>
        bool IsDeleted { get; set; }
    }
}