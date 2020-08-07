using System;

namespace Shopping.DomainModel.Aggregates.Brands.Interfaces
{
    public interface IBrandDomainService
    {
        void CheckBrandName(string name, string latinName);
        void CheckEditedBrandName(Guid id, string name, string latinName);
    }
}