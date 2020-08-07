using System;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.Implements.ProductsSuggestions;

namespace Shopping.QueryModel.QueryModels.ProductsSuggestions.Abstract
{
    public interface IProductSuggestionDto
    {
        string Title { get; set; }
        string ProductImage { get; set; }
        ProductSuggestionStatus ProductSuggestionStatus { get; set; }
        string Description { get; set; }
        DateTime CreationTime { get; set; }
        ProductSuggestionGroupDto ProductSuggestionGroup { get; set; }
        string ProductSuggestionStatusDescription { get; set; }
        string FullName { get; set; }
    }
}