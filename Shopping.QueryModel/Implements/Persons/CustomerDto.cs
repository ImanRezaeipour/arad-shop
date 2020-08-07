using System;
using Shopping.QueryModel.QueryModels.Persons.Customer;

namespace Shopping.QueryModel.Implements.Persons
{
    public class CustomerDto: ICustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public string MobileNumber { get; set; }
        public long PersonNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public string BirthDate { get; set; }
    }
}