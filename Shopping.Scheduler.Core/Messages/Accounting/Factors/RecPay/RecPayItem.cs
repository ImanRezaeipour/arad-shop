namespace Shopping.Scheduler.Core.Messages.Accounting.Factors.RecPay
{
    public class RecPayItem
    {
        public int RPID { get; set; }
        public long DetailCode1 { get; set; }
        public decimal Price { get; set; }
        public string RowDesc { get; set; }
    }
}