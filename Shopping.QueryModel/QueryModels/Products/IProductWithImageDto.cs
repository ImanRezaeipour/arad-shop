using System.Collections.Generic;
using Shopping.QueryModel.QueryModels.Products.ProductDiscount;

namespace Shopping.QueryModel.QueryModels.Products
{
    public interface IProductWithImageDto:IProductDto
    {
        IList<IProductImageDto> ProductImages { get; set; }
        bool DiscountIsValid { get; set; }
        IProductDiscountBaseDto ProductDiscount { get; set; }
    }
}