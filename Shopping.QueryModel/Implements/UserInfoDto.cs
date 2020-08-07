using System;
using Shopping.QueryModel.QueryModels;

namespace Shopping.QueryModel.Implements
{
    public class UserInfoDto: IUserInfoDto
    {
        public Guid UserId { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
    }
}