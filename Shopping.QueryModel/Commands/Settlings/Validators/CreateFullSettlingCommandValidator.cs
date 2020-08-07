using FluentValidation;
using Shopping.Commands.Commands.Settlings.Commands;

namespace Shopping.Commands.Commands.Settlings.Validators
{
    public class CreateFullSettlingCommandValidator:AbstractValidator<CreateDailysetllingCommand>
    {
        public CreateFullSettlingCommandValidator()
        {
            
        }
    }
}