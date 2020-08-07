namespace Shopping.Scheduler.Core.Messages.Accounting.Factors.GRDB
{
    public class AddOrRed
    {
        /// <summary>
        /// شناسه
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// نوع کسور
        /// </summary>
        public short AddorRed { get; set; }
        /// <summary>
        /// ردیف کسور
        /// </summary>
        public short RowId { get; set; }
        /// <summary>
        /// درصد
        /// </summary>
        public float Per { get; set; }
        /// <summary>
        /// مبلغ
        /// </summary>
        public decimal Price { get; set; }
    }
}