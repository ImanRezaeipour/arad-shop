using System;
using System.Collections.Generic;

namespace Shopping.Scheduler.Core.Messages.Accounting.Factors.GRDB
{
    public class Grdb
    {
        public Guid RequestId { get; set; }
        public long Id { get; set; }
        /// <summary>
        /// 100
        /// </summary>
        public short TypeNo { get; set; }
        /// <summary>
        /// 1397/11/08
        /// </summary>
        public string StrDate { get; set; }
        /// <summary>
        /// shop id
        /// </summary>
        public long DetailCode1 { get; set; }
        /// <summary>
        /// customer id
        /// </summary>
        public long DetailCode2 { get; set; }
        /// <summary>
        /// 100001 bank 
        /// </summary>
        public int DetailCode3 { get; set; }
        /// <summary>
        /// bank reference number
        /// </summary>
        public string Desc1 { get; set; }
        public List<GrdbItem> Items { get; set; }
        public List<AddOrRed> AddReds { get; set; }
    }
}