using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.DomainModel.Aggregates.Products.Interfaces;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Products.Services
{
    public class ProductDomainService : IProductDomainService
    {
        private readonly IRepository<Product> _repository;
        public ProductDomainService(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public void ExistProduct(Guid id)
        {
            var product = _repository.AsQuery().FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new
                    DomainException("محصول یافت نشد");
            }
        }
    }
}