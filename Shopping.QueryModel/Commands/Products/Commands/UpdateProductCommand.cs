using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Products.Validators;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Products.Commands
{
	[Validator(typeof(UpdateProductCommandValidator))]
    public class UpdateProductCommand:ShoppingCommandBase
    {
	    public Guid Id { get; set; }
	    public string Name { get; set; }
	    public string ShortDescription { get; set; }
	    public string Description { get; set; }
	    public decimal Price { get; set; }
	    public Guid BrandId { get; set; }
	    public Guid CategoryId { get; set; }
	    public string MainImage { get; set; }
		public List<ProductImageCommand> ProductImages { get; set; }
	}
}