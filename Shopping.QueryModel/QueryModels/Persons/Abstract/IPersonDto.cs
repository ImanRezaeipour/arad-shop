using System;

namespace Shopping.QueryModel.QueryModels.Persons.Abstract
{
    public interface IPersonDto
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string EmailAddress { get; set; }
        bool IsActive { get; set; }
        Guid UserId { get; set; }
        string MobileNumber { get; set; }
        long PersonNumber { get; set; }
    }
}