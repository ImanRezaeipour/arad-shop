namespace Shopping.Infrastructure.Core.Services.Dto.PaginationDtos
{
    /// <summary>
    /// the interface is defined to standarize to request a limited result
    /// </summary>
    public interface ILimitedResultRequest
    {
        /// <summary>
        /// max except result count.
        /// </summary>
        int MaxResultCount { get; set; }
    }
}