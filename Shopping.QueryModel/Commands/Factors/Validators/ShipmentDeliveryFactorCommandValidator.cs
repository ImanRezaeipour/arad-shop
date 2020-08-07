using FluentValidation;
using Shopping.Commands.Commands.Factors.Commands;

namespace Shopping.Commands.Commands.Factors.Validators
{
    public class ShipmentDeliveryFactorCommandValidator:AbstractValidator<ShipmentDeliveryFactorCommand>
    {
        public ShipmentDeliveryFactorCommandValidator()
        {
            
        }
    }
}