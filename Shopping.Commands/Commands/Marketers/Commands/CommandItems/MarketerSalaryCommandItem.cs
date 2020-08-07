namespace Shopping.Commands.Commands.Marketers.Commands.CommandItems
{
    public class MarketerSalaryCommandItem
    {
        /// <summary>
        /// حقوق ثابت
        /// </summary>
        public decimal FixedSalary { get; set; }
        /// <summary>
        /// درصد سود از فروش
        /// </summary>
        public int InterestRates { get; set; }
    }
}