using System;
using Shopping.QueryModel.Implements.Brands;

namespace Shopping.QueryModel.QueryModels.Products
{
    public interface IProductDto
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string ShortDescription { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        decimal DiscountPrice { get; set; }
        string MainImage { get; set; }
        bool IsActive { get; set; }
        BrandDto Brand { get; set; }
        string CategoryName { get; set; }
        Guid CategoryId { get; set; }
        IFakeProductDiscountDto Discount { get; set; }
    }
}