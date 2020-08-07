using FluentValidation.Attributes;
using Shopping.Commands.Commands.Marketers.Validators;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Marketers.Commands
{
    [Validator(typeof(AddMarketerCommentCommandValidator))]
    public class AddMarketerCommentCommand : ShoppingCommandBase
    {
        public long MarketerId { get; set; }
        public string CommentTitle { get; set; }
    }
}