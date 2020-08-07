using System;
using Shopping.QueryModel.QueryModels.Marketers;

namespace Shopping.QueryModel.Implements.Marketers
{
    public class MarketerDto : IMarketerDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MaxMarketerAllowed { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsActive { get; set; }
        public int SubsetShopCount { get; set; }
        public string Image { get; set; }
        public Guid BarcodeId { get; set; }
    }
}