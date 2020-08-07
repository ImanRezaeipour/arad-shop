using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.ProductsSuggestions.Commands;
using Shopping.Commands.Commands.ProductsSuggestions.Commands.CustomerProductSuggestion;
using Shopping.Commands.Commands.ProductsSuggestions.Commands.ShopProductSuggestion;
using Shopping.Commands.Commands.ProductsSuggestions.Responses;
using Shopping.DomainModel.Aggregates.Brands.Aggregates;
using Shopping.DomainModel.Aggregates.Categories.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates.Abstrct;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.ValueObjects;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.ProductsSuggestions
{
    public class ProductSeggestionCommandHandler : ICommandHandler<RejectProductSuggestionCommand, RejectProductSuggestionCommandResponse>
        , ICommandHandler<AcceptProductSuggestionCommand, AcceptProductSuggestionCommandResponse>
        , ICommandHandler<CreateShopProductSuggestionCommand, CreateShopProductSuggestionCommandResponse>
        , ICommandHandler<CreateCustomerProductSuggestionCommand, CreateCustomerProductSuggestionCommandResponse>
    {
        private readonly IRepository<ProductSuggestion> _repository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<CategoryRoot> _categoryRootRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Category> _categoryRepository;
        public ProductSeggestionCommandHandler(IRepository<ProductSuggestion> repository, IRepository<Person> personRepository, IRepository<CategoryRoot> categoryRootRepository, IRepository<Brand> brandRepository, IRepository<Category> categoryRepository)
        {
            _repository = repository;
            _personRepository = personRepository;
            _categoryRootRepository = categoryRootRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }
        public Task<RejectProductSuggestionCommandResponse> Handle(RejectProductSuggestionCommand command)
        {
            var productSuggestion = _repository.Find(command.Id);
            if (productSuggestion == null)
            {
                throw new DomainException("محصول پیشنهادی یافت نشد");
            }
            productSuggestion.ProductSuggestionStatus = ProductSuggestionStatus.Reject;
            productSuggestion.ProductSuggestionStatusDescription = command.ProductSuggestionStatusDescription;
            return Task.FromResult(new RejectProductSuggestionCommandResponse());
        }
        public Task<AcceptProductSuggestionCommandResponse> Handle(AcceptProductSuggestionCommand command)
        {
            var productSuggestion = _repository.Find(command.Id);
            if (productSuggestion == null)
            {
                throw new DomainException("محصول پیشنهادی یافت نشد");
            }
            productSuggestion.ProductSuggestionStatus = ProductSuggestionStatus.Accept;

            return Task.FromResult(new AcceptProductSuggestionCommandResponse());
        }
        public async Task<CreateShopProductSuggestionCommandResponse> Handle(CreateShopProductSuggestionCommand command)
        {
            var person = await _personRepository.AsQuery().OfType<Shop>().SingleOrDefaultAsync(p => p.UserId == command.UserId);
            if (person == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            var categoryRoot = await _categoryRootRepository.FindAsync(command.CategoryRootId);
            if (categoryRoot == null)
            {
                throw new DomainException("دسته بندی ریشه یافت نشد");
            }
            Guid categoryId = Guid.Empty;
            string categoryName = "";
            var category = await _categoryRepository.FindAsync(command.CategoryId);
            if (category != null)
            {
                categoryId = category.Id;
                categoryName = category.Name;
            }
            Guid brandId = Guid.Empty;
            string brandName = "";
            var brand = await _brandRepository.FindAsync(command.BrandId);
            if (brand != null)
            {
                brandName = brand.Name;
                brandId = brand.Id;
            }
            var productSuggestionGroup = new ProductSuggestionGroup(categoryRoot.Id, categoryRoot.Name,
                categoryId, categoryName, brandId, brandName);
            var productSuggestion = new ShopProductSuggestion(Guid.NewGuid(), command.Title, command.ProductImage,
                command.Description, person.Id, productSuggestionGroup);
            _repository.Add(productSuggestion);
            return new CreateShopProductSuggestionCommandResponse();
        }
        public async Task<CreateCustomerProductSuggestionCommandResponse> Handle(CreateCustomerProductSuggestionCommand command)
        {
            var person =await _personRepository.AsQuery().OfType<Customer>().SingleOrDefaultAsync(p => p.UserId == command.UserId);
            if (person == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            var categoryRoot =await _categoryRootRepository.FindAsync(command.CategoryRootId);
            if (categoryRoot == null)
            {
                throw new DomainException("دسته بندی ریشه یافت نشد");
            }
            Guid categoryId = Guid.Empty;
            string categoryName = "";
            var category = await _categoryRepository.FindAsync(command.CategoryId);
            if (category != null)
            {
                categoryId = category.Id;
                categoryName = category.Name;
            }
            Guid brandId = Guid.Empty;
            string brandName = "";
            var brand = await _brandRepository.FindAsync(command.BrandId);
            if (brand != null)
            {
                brandName = brand.Name;
                brandId = brand.Id;
            }
            var productSuggestionGroup = new ProductSuggestionGroup(categoryRoot.Id, categoryRoot.Name,
                categoryId, categoryName, brandId, brandName);
            var productSuggestion = new CustomerProductSuggestion(Guid.NewGuid(), command.Title, command.ProductImage,
                command.Description, person.Id, productSuggestionGroup);
            _repository.Add(productSuggestion);
            return new CreateCustomerProductSuggestionCommandResponse();
        }
    }
}