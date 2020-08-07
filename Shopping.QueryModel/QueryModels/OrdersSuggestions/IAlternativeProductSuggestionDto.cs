using System;

namespace Shopping.QueryModel.QueryModels.OrdersSuggestions
{
    public interface IAlternativeProductSuggestionDto
    {
        Guid ProductId { get; set; }
        string Name { get; set; }
        string ProductImage { get; set; }
        Guid BrandId { get; set; }
        string BrandName { get; set; }
    }
}