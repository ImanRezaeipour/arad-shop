namespace Shopping.Scheduler.Core.Messages.Accounting.Factors.GRDB
{
    public class GrdbItem
    {
        /// <summary>
        /// 301001
        /// </summary>
        public string GoodCode { get; set; }

        /// <summary>
        /// count
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// product title
        /// </summary>
        public string ItemDesc1 { get; set; }

        /// <summary>
        /// amount
        /// </summary>
        public decimal Fee { get; set; }
    }
}