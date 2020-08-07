using System;

namespace Shopping.QueryModel.QueryModels.Products
{
    public interface IProductImageDto
    {
        Guid Id { get; set; }
        string Url { get; set; }
        int Order { get; set; }
    }
}