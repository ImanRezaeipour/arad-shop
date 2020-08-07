namespace Shopping.Infrastructure.Core.Services.Dto.PaginationDtos
{
    /// <summary>
    /// Simply implements <see cref="ILimitedResultRequest"/>.
    /// </summary>
    public class LimitedResultRequestDto: ILimitedResultRequest
    {
        public int MaxResultCount { get; set; }
    }
}