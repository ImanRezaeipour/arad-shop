using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.ProductsSuggestions.Commands.Abstract
{
    public abstract class ProductSuggestionCommandBase : ShoppingCommandBase
    {
        public string Title { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
        public Guid CategoryRootId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public Guid UserId { get; set; }
    }
}