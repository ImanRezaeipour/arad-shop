using Shopping.Infrastructure.Core.Enums;

namespace Shopping.Infrastructure.SeedWorks
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object ResponseData { get; set; }
        public ErrorCode ErrorCode { get; set; }
    }
}