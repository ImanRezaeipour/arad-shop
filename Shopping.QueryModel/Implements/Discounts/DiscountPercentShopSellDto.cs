using Shopping.QueryModel.Implements.Persons;

namespace Shopping.QueryModel.Implements.Discounts
{
    public class DiscountPercentShopSellDto
    {
        public ShopDto Shop { get; set; }
        public decimal Debit { get; set; }
    }
}