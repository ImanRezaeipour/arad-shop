using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements.ProductsSuggestions;
using Shopping.QueryModel.QueryModels.ProductsSuggestions.CustomerProductsSuggestion;
using Shopping.QueryService.Interfaces.ProductsSuggestion;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.ProductsSuggestions
{
    public class CustomerProductsSuggestionQueryService : ICustomerProductsSuggestionQueryService
    {
        private readonly IReadOnlyRepository<CustomerProductSuggestion, Guid> _repository;
        private readonly IReadOnlyRepository<Person, Guid> _personRepository;
        public CustomerProductsSuggestionQueryService(IReadOnlyRepository<CustomerProductSuggestion, Guid> repository, IReadOnlyRepository<Person, Guid> personRepository)
        {
            _repository = repository;
            _personRepository = personRepository;
        }
        public ICustomerProductSuggestionDto GetById(Guid id)
        {
            var productSuggestion = _repository.Find(id);
            var  result= productSuggestion.ToDto();
            var person = _personRepository.Find(productSuggestion.PersonId);
            result.FullName = person.FirstName + person.LastName;
            return result;
        }
        public IQueryable<CustomerProductSuggestionDto> GetAll()
        {
            var customers = _personRepository.AsQuery().OfType<Customer>();
            var productSuggestions = _repository.AsQuery();
            var join = productSuggestions.Join(customers, ps => ps.PersonId, s => s.Id, (psu, shop) => new
            {
                ProductSuggestion = psu,
                customer = shop
            });
            return join.Select(item => new CustomerProductSuggestionDto
            {
                CreationTime = item.ProductSuggestion.CreationTime,
                Id = item.ProductSuggestion.Id,
                Title = item.ProductSuggestion.Title,
                Description = item.ProductSuggestion.Description,
                ProductSuggestionStatusDescription = item.ProductSuggestion.ProductSuggestionStatusDescription,
                ProductImage = item.ProductSuggestion.ProductImage,
                FullName = item.customer.FirstName + item.customer.LastName,
                ProductSuggestionGroup = new ProductSuggestionGroupDto
                {
                    BrandName = item.ProductSuggestion.ProductSuggestionGroup.BrandName,
                    BrandId = item.ProductSuggestion.ProductSuggestionGroup.BrandId,
                    CategoryId = item.ProductSuggestion.ProductSuggestionGroup.CategoryId,
                    CategoryName = item.ProductSuggestion.ProductSuggestionGroup.CategoryName,
                    CategoryRootId = item.ProductSuggestion.ProductSuggestionGroup.CategoryRootId,
                    CategoryRootName = item.ProductSuggestion.ProductSuggestionGroup.CategoryRootName
                },
                ProductSuggestionStatus = item.ProductSuggestion.ProductSuggestionStatus,
            }); ;
        }
        public MobilePagingResultDto<ICustomerProductSuggestionDto> GetByUserId(Guid userId, PagedInputDto pagedInput)
        {
            var person = _personRepository.AsQuery().OfType<Customer>().SingleOrDefault(p => p.UserId == userId);
            if (person == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            var data = _repository.AsQuery()
                .Where(p => p.PersonId == person.Id);
            var result = data.OrderByDescending(item => item.CreationTime)
                    .Skip(pagedInput.Skip)
                    .Take(pagedInput.Count)
                    .ToList()
                    .Select(item => item.ToDto())
                    .ToList();
            return new MobilePagingResultDto<ICustomerProductSuggestionDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
    }
}