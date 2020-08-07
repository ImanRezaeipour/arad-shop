using Shopping.QueryModel.QueryModels.Persons.Shop.CustomerSubsets;

namespace Shopping.QueryModel.Implements.Persons.CustomerSubsets
{
    public class ShopsCustomerSubsetReportDto : IShopsCustomerSubsetReportDto
    {
        /// <summary>
        /// کد فروشگاه
        /// </summary>
        public long PersonNumber { get; set; }
        /// <summary>
        /// نام فروشگاه
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// نام 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// نام بازاریاب
        /// </summary>
        public string MarketerFullName { get; set; }
        /// <summary>
        /// تعداد کل جذب
        /// </summary>
        public int CustomerSubsetsCount { get; set; }
        /// <summary>
        /// تعداد کل منجر به خرید
        /// </summary>
        public int CustomerSubsetsHavePaidFactorCount { get; set; }
        /// <summary>
        /// تعداد تسویه شده
        /// </summary>
        public int CustomerSubsetsSettlementCount { get; set; }
        /// <summary>
        /// تعداد تسویه نشده
        /// </summary>
        public int CustomerSubsetsNotSettlementCount { get; set; }
    }
}