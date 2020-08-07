using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Marketers.ValueObjects
{
    /// <summary>
    /// حقوق بازاریاب
    /// </summary>
    public class MarketerSalary : ValueObject<MarketerSalary>
    {
        protected MarketerSalary()
        {
        }
        public MarketerSalary(decimal fixedSalary, int interestRates)
        {
            if (fixedSalary < 1)
            {
                throw new DomainException("حقوق ثابت بازاریاب نمیتواند کمتر از یک باشد");
            }
            if (interestRates < 0 || interestRates > 100)
            {
                throw new DomainException("درصد بازایاب باید بین صفر تا صد باشد");
            }
            FixedSalary = fixedSalary;
            InterestRates = interestRates;
        }
        /// <summary>
        /// حقوق ثابت
        /// </summary>
        public decimal FixedSalary { get; private set; }
        /// <summary>
        /// درصد سود از فروش
        /// </summary>
        public int InterestRates { get; private set; }
    }
}