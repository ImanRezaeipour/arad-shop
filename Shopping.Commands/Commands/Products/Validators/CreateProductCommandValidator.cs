using FluentValidation;
using Shopping.Commands.Commands.Products.Commands;

namespace Shopping.Commands.Commands.Products.Validators
{
	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(p => p.Name).NotEmpty().WithMessage("نام نمی تواند خالی باشد");
			
			RuleFor(p => p.Price).GreaterThan(0).WithMessage("قیمت نمی تواند کمتر از صفر باشد");
		}
	}
}