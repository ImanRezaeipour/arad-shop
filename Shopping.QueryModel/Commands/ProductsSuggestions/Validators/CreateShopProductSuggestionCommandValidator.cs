using FluentValidation;
using Shopping.Commands.Commands.ProductsSuggestions.Commands.ShopProductSuggestion;

namespace Shopping.Commands.Commands.ProductsSuggestions.Validators
{
    public class CreateShopProductSuggestionCommandValidator:AbstractValidator<CreateShopProductSuggestionCommand>
    {
        public CreateShopProductSuggestionCommandValidator()
        {
            RuleFor(p => p.ProductImage).NotEmpty().WithMessage("عکس محصول نمی تواند خالی باشد");
            RuleFor(p => p.Title).NotEmpty().WithMessage("عناون نمی تواند خالی باشد");
        }
    }
}