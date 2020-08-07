using System;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Products.Validators;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Products.Commands
{
	[Validator(typeof(DeActiveProductCommandValidator))]
	public class DeActiveProductCommand: ShoppingCommandBase
	{
		public Guid Id { get; set; }
	}
}