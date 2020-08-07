using Shopping.QueryModel.Implements.Persons;

namespace Shopping.QueryModel.Implements.Orders
{
    public class AreaOrderWithShopDto: OrderBaseDto
    {
        public ShopDto Shop { get; set; }
    }
}