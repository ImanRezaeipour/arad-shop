using System;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Categories.Commands.Items;
using Shopping.Commands.Commands.Categories.Validators;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Categories.Commands
{
    [Validator(typeof(UpdateCategoryCommandValidator))]
    public class UpdateCategoryCommand: ShoppingCommandBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryImageCommand CategoryImage { get; set; }
    }
}