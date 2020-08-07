using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;

namespace Shopping.DomainModel.Aggregates.Orders.Interfaces
{
    public interface IOrderDomainService
    {
        void CheckOrderDiscount(Customer customer);
        OrderBase CalcOrderPercentDiscount(OrderBase order);
        bool HavePercentDiscountToday(Customer customer);
        int CountPercentDiscountToday(Customer customer);
        bool CheckOrderPercentDiscountToday(Customer customer, long orderId);
        Task CheckCustomerRequestOrderDuration(Customer customer, ApplicationSetting setting);
    }
}