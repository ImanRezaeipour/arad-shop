using System;
using System.Collections.Generic;

namespace Shopping.Scheduler.Core.Messages.Accounting.Factors.RecPay
{
    public class FactorRecPay
    {
        public Guid RequestId { get; set; }
        public string Desc { get; set; }
        public int DetailCode2 { get; set; }
        public int RPCode { get; set; }
        public int DevId { get; set; }
        public string strDate { get; set; }
        public string CheqNo { get; set; }
        public List<RecPayItem> RecPayItems { get; set; }
    }
}