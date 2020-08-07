using System;

namespace Shopping.QueryModel.QueryModels.Orders
{
    public interface IOrderProductDto
    {
        Guid ProductId { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        string ProductImage { get; set; }
        Guid BrandId { get; set; }
        string BrandName { get; set; }
    }
}