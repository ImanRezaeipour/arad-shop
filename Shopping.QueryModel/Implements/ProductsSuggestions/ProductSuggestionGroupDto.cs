using System;

namespace Shopping.QueryModel.Implements.ProductsSuggestions
{
    public class ProductSuggestionGroupDto
    {
        public Guid CategoryRootId { get; set; }
        public string CategoryRootName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
    }
}