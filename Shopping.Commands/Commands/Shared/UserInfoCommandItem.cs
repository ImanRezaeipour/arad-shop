using System;

namespace Shopping.Commands.Commands.Shared
{
    public class UserInfoCommandItem
    {
        private UserInfoCommandItem()
        {
        }

        public UserInfoCommandItem(Guid userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}