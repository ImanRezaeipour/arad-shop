using System;

namespace Shopping.DomainModel.Aggregates.Categories.Interfaces
{
    public interface ICategoryDomainService
    {
        void CheckCategoryName(string name);
        void CheckCategoryName(Guid id, string name);
    }
}