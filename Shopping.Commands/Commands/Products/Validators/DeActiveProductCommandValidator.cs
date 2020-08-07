using FluentValidation;
using Shopping.Commands.Commands.Products.Commands;

namespace Shopping.Commands.Commands.Products.Validators
{
	public class DeActiveProductCommandValidator:AbstractValidator<DeActiveProductCommand>
	{
		public DeActiveProductCommandValidator()
		{
			
		}
	}
}