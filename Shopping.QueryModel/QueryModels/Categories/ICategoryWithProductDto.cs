using System.Collections.Generic;
using Shopping.QueryModel.QueryModels.Categories.Abstract;
using Shopping.QueryModel.QueryModels.Products;

namespace Shopping.QueryModel.QueryModels.Categories
{
    public interface ICategoryWithProductDto : ICategoryBaseWithImageDto
    {
        IList<IProductDto> Products { get; set; }
    }
}