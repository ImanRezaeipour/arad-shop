using Shopping.QueryModel.QueryModels.Products.ProductDiscount;

namespace Shopping.QueryModel.QueryModels.Products
{
    public interface IProductWithDiscountDto : IProductDto
    {
        bool DiscountIsValid { get; set; }
        IProductDiscountBaseDto ProductDiscount { get; set; }
    }
}