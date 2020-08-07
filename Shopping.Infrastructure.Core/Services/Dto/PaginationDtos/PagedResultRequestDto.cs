using System;

namespace Shopping.Infrastructure.Core.Services.Dto.PaginationDtos
{
    /// <summary>
    /// Simply implements <see cref="IPagedResultRequest"/>.
    /// </summary>
    [Serializable]
    public class PagedResultRequestDto : LimitedResultRequestDto, IPagedResultRequest
    {
        public virtual int SkipCount { get; set; }
    }
}