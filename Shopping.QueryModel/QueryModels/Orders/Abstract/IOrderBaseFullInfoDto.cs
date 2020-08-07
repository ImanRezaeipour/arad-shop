using System.Collections.Generic;
using Shopping.QueryModel.QueryModels.Orders.Items;
using Shopping.QueryModel.QueryModels.Persons.Customer;

namespace Shopping.QueryModel.QueryModels.Orders.Abstract
{
    public interface IOrderBaseFullInfoDto : IOrderBaseDto
    {
        IList<IOrderItemDto> OrderItems { get; set; }
        IOrderAddressDto OrderAddress { get; set; }
        ICustomerDto Customer { get; set; }
        int ItemsCount { get; set; }
        decimal TotalPrice { get; set; }
    }
}