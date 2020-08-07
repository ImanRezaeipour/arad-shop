namespace Shopping.QueryModel.QueryModels.Marketers
{
    public interface IMarketerReagentDto
    {
        /// <summary>
        /// نام معرف
        /// </summary>
        string ReagentName { get; set; }
        /// <summary>
        /// شماره همراه معرف
        /// </summary>
        string ReagentMobileNumber { get; set; }
    }
}