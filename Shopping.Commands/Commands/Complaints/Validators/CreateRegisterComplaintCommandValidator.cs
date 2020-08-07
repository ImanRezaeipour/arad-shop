using FluentValidation;
using Shopping.Commands.Commands.Complaints.Commands;

namespace Shopping.Commands.Commands.Complaints.Validators
{
    public class CreateRegisterComplaintCommandValidator:AbstractValidator<CreateRegisterComplaintCommand>
    {
        public CreateRegisterComplaintCommandValidator()
        {
            
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("نام نمی تواند خالی باشد");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("نام خانوادگی نمی تواند خالی باشد");
            RuleFor(p => p.Title).NotEmpty().WithMessage("عنوان نمی تواند خالی باشد");
            RuleFor(p => p.Email).NotEmpty().WithMessage("ایمیل نم تواند خالی باشد");
            
        }
    }
}