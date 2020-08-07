using System;

namespace Shopping.QueryModel.QueryModels.Promoters
{
    public interface IPromoterDto
    {
        Guid Id { get; set; }
        long Code { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MobileNumber { get; set; }
        string NationalCode { get; set; }
        DateTime CreationTime { get; set; }
        int CustomerSubsetCount { get; set; }
        int CustomerSubsetHavePaidFactorCount { get; set; }
    }
}