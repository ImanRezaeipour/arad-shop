using Shopping.QueryModel.Implements.Persons;

namespace Shopping.QueryModel.Implements.Orders
{
    public class OrderBaseWithCustomerDto : OrderBaseDto
    {
        public CustomerDto Customer { get; set; }
    }
}