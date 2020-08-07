using System.Collections.Generic;

namespace Shopping.Infrastructure.SeedWorks
{
    public class MobilePagingResultDto<TDto>
    {
        public IEnumerable<TDto> Result { get; set; }
        public int Count { get; set; }
    }
}