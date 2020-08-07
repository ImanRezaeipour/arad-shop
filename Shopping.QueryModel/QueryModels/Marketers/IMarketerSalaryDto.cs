namespace Shopping.QueryModel.QueryModels.Marketers
{
    public interface IMarketerSalaryDto
    {
        /// <summary>
        /// حقوق ثابت
        /// </summary>
        decimal FixedSalary { get; set; }
        /// <summary>
        /// درصد سود از فروشS
        /// </summary>
        int InterestRates { get; set; }
    }
}