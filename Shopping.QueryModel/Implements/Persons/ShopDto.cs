using System;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.Persons.Shop;

namespace Shopping.QueryModel.Implements.Persons
{
    public class ShopDto : IShopDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public string MobileNumber { get; set; }
        public long PersonNumber { get; set; }
        public string Name { get; set; }
        public ShopStatus ShopStatus { get; set; }
        public string DescriptionStatus { get; set; }
        public string Description { get; set; }
        public string NationalCode { get; set; }
        public int DefaultDiscount { get; set; }
        public DateTime CreationTime { get; set; }
        public long? MarketerId { get; set; }
        public int AreaRadius { get; set; }
        public int Metrage { get; set; }
        public bool HasMarketer { get; set; }
        public string MarketerFullName { get; set; }
    }
}