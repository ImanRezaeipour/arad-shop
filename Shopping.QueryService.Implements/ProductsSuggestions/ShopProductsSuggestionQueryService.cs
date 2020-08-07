using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements.ProductsSuggestions;
using Shopping.QueryModel.QueryModels.ProductsSuggestions.ShopProductsSuggestion;
using Shopping.QueryService.Interfaces.ProductsSuggestion;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.ProductsSuggestions
{
    public class ShopProductsSuggestionQueryService : IShopProductsSuggestionQueryService
    {
        private readonly IReadOnlyRepository<Shop, Guid> _personRepository;
        private readonly IReadOnlyRepository<ShopProductSuggestion, Guid> _repository;
        public ShopProductsSuggestionQueryService(IReadOnlyRepository<ShopProductSuggestion, Guid> repository, IReadOnlyRepository<Shop, Guid> personRepository)
        {
            _repository = repository;
            _personRepository = personRepository;
        }
        public IShopProductSuggestionDto GetById(Guid id)
        {
            var productSuggestion = _repository.Find(id);
            var result = productSuggestion.ToDto();
            var person = _personRepository.Find(productSuggestion.PersonId);
            result.FullName = person.FirstName + person.LastName;
            result.ShopName = person.Name;
            return result;
        }
        public IQueryable<ShopProductSuggestionDto> GetAll()
        {
            var shops = _personRepository.AsQuery();
            var productSuggestions = _repository.AsQuery();
            var join = productSuggestions.Join(shops, ps => ps.PersonId, s => s.Id, (psu, shop) => new
            {
                ProductSuggestion = psu,
                Shop = shop
            });
            return join.Select(item => new ShopProductSuggestionDto
            {
                CreationTime = item.ProductSuggestion.CreationTime,
                Id = item.ProductSuggestion.Id,
                Title = item.ProductSuggestion.Title,
                Description = item.ProductSuggestion.Description,
                ProductSuggestionStatusDescription = item.ProductSuggestion.ProductSuggestionStatusDescription,
                ProductImage = item.ProductSuggestion.ProductImage,
                FullName = item.Shop.FirstName + item.Shop.LastName,
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
                ShopName = item.Shop.Name
            });
        }
        public MobilePagingResultDto<IShopProductSuggestionDto> GetByUserId(Guid userId, PagedInputDto pagedInput)
        {
            var person = _personRepository.AsQuery().OfType<Shop>().SingleOrDefault(p => p.UserId == userId);
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
            return new MobilePagingResultDto<IShopProductSuggestionDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
    }
}