using System;
using Shopping.Commands.Commands.Persons.Commands.Abstract;

namespace Shopping.Commands.Commands.Persons.Commands.Customer
{
    public class CreateCustomerCommand : PersonCommand
    {
        public DateTime BirthDate { get; set; }
        public long? RecommendCode { get; set; }
    }
}