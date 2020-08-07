using System;

namespace Shopping.QueryModel.Implements.Promoters
{
    public class PromoterDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public int CustomerSubsetCount { get; set; }
        public int CustomerSubsetHavePaidFactorCount { get; set; }
    }
}