using System;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Persons.Commands.Abstract;
using Shopping.Commands.Commands.Persons.Validators;

namespace Shopping.Commands.Commands.Persons.Commands.Shop
{
    [Validator(typeof(CreateShopCommandValidator))]
    public class CreateShopCommand : PersonCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string NationalCode { get; set; }
        public ShopAddressCommand Address { get; set; }
        public BankAccountCommand BankAccount { get; set; }
        public ImageDocumentsCommand ImageDocuments { get; set; }
        public int AreaRadius { get; set; }
        public int Metrage { get; set; }
        public int DefaultDiscount { get; set; }
        public Guid BarcodeId { get; set; }
    }
}