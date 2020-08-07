using FluentValidation.Attributes;
using Shopping.Commands.Commands.Categories.Commands.Abstact;
using Shopping.Commands.Commands.Categories.Commands.Items;
using Shopping.Commands.Commands.Categories.Validators;

namespace Shopping.Commands.Commands.Categories.Commands
{
    [Validator(typeof(CreateCategoryRootCommandValidator))]
    public class CreateCategoryRootCommand : CategoryBaseCommand
    {
        public CategoryImageCommand CategoryImage { get; set; }
    }
}