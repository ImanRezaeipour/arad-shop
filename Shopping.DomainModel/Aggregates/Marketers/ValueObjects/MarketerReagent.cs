using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Marketers.ValueObjects
{
    /// <summary>
    /// معرف بازاریاب
    /// </summary>
    public class MarketerReagent : ValueObject<MarketerReagent>
    {
        protected MarketerReagent() { }
        public MarketerReagent(string reagentName, string reagentMobileNumber)
        {
            ReagentName = reagentName;
            ReagentMobileNumber = reagentMobileNumber;
        }
        /// <summary>
        /// نام معرف
        /// </summary>
        public string ReagentName { get; private set; }
        /// <summary>
        /// شماره همراه معرف
        /// </summary>
        public string ReagentMobileNumber { get; private set; }
    }
}