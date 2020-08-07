using System;

namespace Shopping.QueryService.Implements.Accounting
{
    public class ShopStatementServiceRequest
    {
        public long DetailCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Guid RequestId { get; set; }
    }
}