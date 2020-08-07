using System;

namespace Shopping.DomainModel.Aggregates.Products.Interfaces
{
    public interface IProductDomainService
    {
        void ExistProduct(Guid id);
    }
}