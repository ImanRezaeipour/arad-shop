using Shopping.DomainModel.Aggregates.Persons.Aggregates;

namespace Shopping.DomainModel.Aggregates.Factors.Interfaces
{
    public interface IFactorDomainService
    {
        void FactorIsExist(long orderId);
        bool HavePercentDiscountToday(Customer customer);
        bool HasFirstBuy(Customer customer);
    }
}
