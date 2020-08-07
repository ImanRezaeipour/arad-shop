using Shopping.DomainModel.Aggregates.Persons.Aggregates;

namespace Shopping.DomainModel.Aggregates.Orders.DiscountRules.Interfaces
{
    public interface IOrderDiscountRule
    {
        void CheckCustomerDiscount(Customer customer);
    }
}