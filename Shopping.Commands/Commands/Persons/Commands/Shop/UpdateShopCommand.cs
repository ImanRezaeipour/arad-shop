using System;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Persons.Commands.Shop.Item;
using Shopping.Commands.Commands.Persons.Validators;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Commands.Shop
{
    [Validator(typeof(UpdateShopCommandValidator))]
    public class UpdateShopCommand : ShoppingCommandBase
    {
        public Guid ShopId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NationalCode { get; set; }
        public int AreaRadius { get; set; }
        public int Metrage { get; set; }
        public ImageDocumentsCommand ImageDocuments { get; set; }
        public BankAccountCommand BankAccount { get; set; }
        public ShopAddressCommandItem ShopAddress { get; set; }
    }
}