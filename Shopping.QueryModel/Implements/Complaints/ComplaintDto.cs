using System;
using Shopping.QueryModel.Implements.Persons;

namespace Shopping.QueryModel.Implements.Complaints
{
    public class ComplaintDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ShopDto Shop { get; set; }
        public bool Tracked { get; set; }
        public DateTime CreationTime { get; set; }
    }
}