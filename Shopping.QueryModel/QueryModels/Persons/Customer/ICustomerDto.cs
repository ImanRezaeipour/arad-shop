using System;
using Shopping.QueryModel.QueryModels.Persons.Abstract;

namespace Shopping.QueryModel.QueryModels.Persons.Customer
{
    public interface ICustomerDto : IPersonDto
    {
        DateTime CreationTime { get; set; }
        string BirthDate { get; set; }
    }
}