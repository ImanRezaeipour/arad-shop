using System;

namespace Shopping.QueryModel.QueryModels.ProductsSuggestions
{
    public interface IProductSuggestionGroupDto
    {
        Guid CategoryRootId { get; set; }
        string CategoryRootName { get; set; }
        Guid CategoryId { get; set; }
        string CategoryName { get; set; }
        Guid BrandId { get; set; }
        string BrandName { get; set; }
    }
}