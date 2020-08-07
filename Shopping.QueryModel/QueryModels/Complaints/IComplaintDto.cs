using System;
using Shopping.QueryModel.QueryModels.Persons.Shop;

namespace Shopping.QueryModel.QueryModels.Complaints
{
    public interface IComplaintDto
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        IShopDto Shop { get; set; }
        bool Tracked { get; set; }
        DateTime CreationTime { get; set; }
    }
}