using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.Brands.Aggregates;
using Shopping.DomainModel.Aggregates.Brands.Interfaces;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Brands.Services
{
    public class BrandDomainService : IBrandDomainService
    {
        private readonly IRepository<Brand> _repository;
        public BrandDomainService(IRepository<Brand> repository)
        {
            _repository = repository;
        }
        public void CheckBrandName(string name, string latinName)
        {
            var isExist =
                _repository.AsQuery().Any(item => item.Name == name ||
                                                  item.LatinName == latinName);
            if (isExist)
            {
                throw new DomainException("عنوان انتخابی شما قبلا ثبت شده است");
            }
        }

        public void CheckEditedBrandName(Guid id, string name, string latinName)
        {
            var isExist =
                _repository.AsQuery()
                    .Any(item => item.Id != id && (item.Name == name || item.LatinName == latinName));
            if (isExist)
            {
                throw new DomainException("عنوان انتخابی شما قبلا ثبت شده است");
            }
        }
    }
}