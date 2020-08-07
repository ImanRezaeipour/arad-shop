using System;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;

namespace Shopping.DomainModel.Aggregates.Discounts.Interfaces
{
    public interface IPercentDiscountDomainService
    {
        Task CheckPercentDiscountDate(DateTime fromDate, DateTime toDate);
        void ValidationSettingDiscount(int maxOrderCount, int discountMaxOrderCount, int maxProductCount, int discountMaxProductCount);
        bool HaveRemainOrderCount(Guid percentDiscountId, Customer customer);
        void LowOfNumberRemainOrderCount(Guid percenrDiscountId, Customer customer);
        void AddDiscountSellToPercentDiscount(Factor factor);
    }
}