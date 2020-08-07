namespace Shopping.QueryModel.QueryModels.Persons.Shop.CustomerSubsets
{
    public interface IShopsCustomerSubsetReportDto
    {
        /// <summary>
        /// کد فروشگاه
        /// </summary>
        long PersonNumber { get; set; }
        /// <summary>
        /// نام فروشگاه
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// نام 
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        string MobileNumber { get; set; }
        /// <summary>
        /// نام بازاریاب
        /// </summary>
        string MarketerFullName { get; set; }
        /// <summary>
        /// تعداد کل جذب
        /// </summary>
        int CustomerSubsetsCount { get; set; }
        /// <summary>
        /// تعداد کل منجر به خرید
        /// </summary>
        int CustomerSubsetsHavePaidFactorCount { get; set; }
        /// <summary>
        /// تعداد تسویه شده
        /// </summary>
        int CustomerSubsetsSettlementCount { get; set; }
        /// <summary>
        /// تعداد تسویه نشده
        /// </summary>
        int CustomerSubsetsNotSettlementCount { get; set; }
    }
}