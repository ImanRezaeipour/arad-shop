namespace Shopping.Infrastructure.Core.Services.Dto.PaginationDtos
{
    /// <summary>
    /// This interface is defined to standardize to request a paged result.
    /// </summary>
    public interface IPagedResultRequest : ILimitedResultRequest
    {
        /// <summary>
        /// skip count (beginning of the page).
        /// </summary>
        int SkipCount { get; set; }
    }
}