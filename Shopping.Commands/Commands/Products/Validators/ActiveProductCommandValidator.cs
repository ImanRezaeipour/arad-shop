using FluentValidation;
using Shopping.Commands.Commands.Products.Commands;

namespace Shopping.Commands.Commands.Products.Validators
{
	public class ActiveProductCommandValidator:AbstractValidator<ActiveProductCommand>
	{
		public ActiveProductCommandValidator()
		{
			
		}
	}
}