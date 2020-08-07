using System;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Categories.Commands.Abstact;
using Shopping.Commands.Commands.Categories.Validators;

namespace Shopping.Commands.Commands.Categories.Commands
{
    [Validator(typeof(CreateCategoryCommandValidator))]
    public class CreateCategoryCommand : CategoryBaseCommand
    {
        public Guid ParentCategory { get; set; }
    }
}