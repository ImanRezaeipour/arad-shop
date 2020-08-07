﻿using Shopping.Commands.Commands.Persons.Commands.Abstract;

namespace Shopping.Commands.Commands.Persons.Commands.Shop
{
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
    }
}