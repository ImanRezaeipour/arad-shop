using FluentValidation;
using Shopping.Commands.Commands.ProductsSuggestions.Commands.CustomerProductSuggestion;

namespace Shopping.Commands.Commands.ProductsSuggestions.Validators
{
    public class CreateCustomerProductSuggestionCommandValidator:AbstractValidator<CreateCustomerProductSuggestionCommand>
    {
        public CreateCustomerProductSuggestionCommandValidator()
        {
            RuleFor(p => p.ProductImage).NotEmpty().WithMessage("عکس محصول نمی تواند خالی باشد");
            RuleFor(p => p.Title).NotEmpty().WithMessage("عناون نمی تواند خالی باشد");
           
        }
    }
}