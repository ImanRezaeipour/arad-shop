using FluentValidation;
using FluentValidation.Attributes;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands
{
    [Validator(typeof(TestValidator))]
    public class TestCommand : ShoppingCommandBase
    {
        public string Test { get; set; }
        public long Id { get; set; }
    }

    public class TestValidator : AbstractValidator<TestCommand>
    {
        public TestValidator()
        {
            RuleFor(item => item.Id).GreaterThan(0).WithMessage("شناسه بزرگتر از صفر");
            RuleFor(item => item.Test).NotNull().WithMessage("adbsahksdf");
        }
    }
}