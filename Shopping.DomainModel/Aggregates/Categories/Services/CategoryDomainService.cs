using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Categories.Interfaces;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Categories.Services
{
    public class CategoryDomainService : ICategoryDomainService
    {
        private readonly IRepository<CategoryBase> _repository;
        public CategoryDomainService(IRepository<CategoryBase> repository)
        {
            _repository = repository;
        }
        public void CheckCategoryName(string name)
        {
            var isExist = _repository.AsQuery().Any(p => p.Name == name);
            if (isExist)
            {
                throw new DomainException("نام دسته بندی موجود می باشد");
            }
        }

        public void CheckCategoryName(Guid id, string name)
        {
            var isExist = _repository.AsQuery().Any(p => p.Id != id && p.Name == name);
            if (isExist)
            {
                throw new DomainException("نام دسته انتخابی موجود می باشد");
            }
        }
    }
}