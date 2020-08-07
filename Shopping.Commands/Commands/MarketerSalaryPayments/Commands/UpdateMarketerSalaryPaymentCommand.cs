using System;
using Shopping.Commands.Commands.MarketerSalaryPayments.Commands.CommandItems;
using Shopping.Commands.Commands.Shared;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.MarketerSalaryPayments.Commands
{
    public class UpdateMarketerSalaryPaymentCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public long MarketerId { get; set; }
        public PeriodSalaryCommand PeriodSalaryCommand { get; set; }
        public UserInfoCommandItem UserInfoCommand { get; set; }
    }
}