using System;

namespace Shopping.QueryModel.QueryModels
{
    public interface IUserInfoDto
    {
        Guid UserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}