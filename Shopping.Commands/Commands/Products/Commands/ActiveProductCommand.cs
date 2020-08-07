using System;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Products.Validators;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Products.Commands
{
	[Validator(typeof(ActiveProductCommandValidator))]
	public class ActiveProductCommand:ShoppingCommandBase
	{
		public Guid Id { get; set; }
	}
}