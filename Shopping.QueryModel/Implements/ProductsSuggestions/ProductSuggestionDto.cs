using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.Implements.ProductsSuggestions
{
    public class ProductSuggestionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ProductImage { get; set; }
        public ProductSuggestionStatus ProductSuggestionStatus { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public ProductSuggestionGroupDto ProductSuggestionGroup { get; set; }
        public string ProductSuggestionStatusDescription { get; set; }
        public string FullName { get; set; }
    }
}