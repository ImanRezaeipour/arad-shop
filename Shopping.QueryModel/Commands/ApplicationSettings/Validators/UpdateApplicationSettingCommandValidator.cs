using FluentValidation;
using Shopping.Commands.Commands.ApplicationSettings.Commands;

namespace Shopping.Commands.Commands.ApplicationSettings.Validators
{
    public class UpdateApplicationSettingCommandValidator:AbstractValidator<UpdateApplicationSettingCommand>
    {
        public UpdateApplicationSettingCommandValidator()
        {
            RuleFor(p => p.OrderExpierTime).GreaterThan(0).WithMessage("زمان انقضاء سفارش نمی تواند صفر باشد");
            RuleFor(p => p.MaximumDeliveryTime).GreaterThan(0).WithMessage("جداکثر زمان تحویل نمی تواند صفر باشد");
            RuleFor(p => p.ShopAppVersion).NotEmpty().WithMessage("ورژن برنامه فرو شگاه نمی تواند خالی باشد");
            RuleFor(p => p.ShopDownloadLink).NotEmpty().WithMessage("لینک دانلود فروشگاه نمی تواند خالی باشد");
            RuleFor(p => p.CustomerAppVersion).NotEmpty().WithMessage("ورژن برنامه مشتری نمی تواند خالی باشد");
            RuleFor(p => p.CustomerDownloadLink).NotEmpty().WithMessage("لینک دانلود مشتری نمی تواند خالی باشد");
            
        }
    }
}