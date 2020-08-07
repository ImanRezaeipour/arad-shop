using System;
using Shopping.QueryModel.Implements.Brands;
using Shopping.QueryModel.Implements.Products.ProductDiscount;

namespace Shopping.QueryModel.Implements.Products
{
    public class ProductDto 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string MainImage { get; set; }
        public bool IsActive { get; set; }
        public BrandDto Brand { get; set; }
        public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public ProductDiscountBaseDto ProductDiscountBase { get; set; }
        public bool DiscountIsValid { get; set; }
    }
}