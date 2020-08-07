using System;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Brands.Validators;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Brands.Commands
{
    [Validator(typeof(UpdateBrandCommandValidator))]
    public class UpdateBrandCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public Guid CategoryRootId { get; set; }
    }
}