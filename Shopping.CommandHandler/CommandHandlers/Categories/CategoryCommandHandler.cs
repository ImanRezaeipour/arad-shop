using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Categories.Commands;
using Shopping.Commands.Commands.Categories.Responses;
using Shopping.DomainModel.Aggregates.Categories.Aggregates;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Categories.Interfaces;
using Shopping.DomainModel.Aggregates.Categories.ValueObjects;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Categories
{
    public class CategoryCommandHandler :
        ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
        , ICommandHandler<CreateCategoryRootCommand, CreateCategoryRootCommandResponse>
        , ICommandHandler<UpdateCategoryRootCommand, UpdateCategoryRootCommandResponse>
        , ICommandHandler<ActiveCategoryCommand, ActiveCategoryCommandResponse>
        , ICommandHandler<DeActiveCategoryCommand, DeActiveCategoryCommandResponse>
        , ICommandHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
        , ICommandHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
        , ICommandHandler<DeleteCategoryRootCommand, DeleteCategoryRootCommandResponse>
        , ICommandHandler<SortSubCategoryCommand, SortCategoryCommandResponse>
        , ICommandHandler<SortCategoryRootCommand, SortCategoryRootCommandResponse>
    {
        private readonly ICategoryDomainService _categoryDomainService;
        private readonly IRepository<CategoryBase> _repository;
        private readonly IRepository<Product> _productRepository;
        public CategoryCommandHandler(ICategoryDomainService categoryDomainService,
            IRepository<CategoryBase> repository,
            IRepository<Product> productRepository)
        {
            _categoryDomainService = categoryDomainService;
            _repository = repository;
            _productRepository = productRepository;
        }
        public Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand command)
        {
            _categoryDomainService.CheckCategoryName(command.Name);
            var parentCategory = _repository.Find(command.ParentCategory);
            if (parentCategory == null)
            {
                throw new DomainException("دسته بندی پدر  یافت نشد");
            }
            var category = new Category(command.Id, command.Name, command.Description,
                parentCategory.SubCategories.Count, CategoryImage.CreateNullImage())
            {
                SubCategories = new List<Category>()
            };
            parentCategory.SubCategories.Add(category);
            return Task.FromResult(new CreateCategoryCommandResponse());
        }
        public Task<CreateCategoryRootCommandResponse> Handle(CreateCategoryRootCommand command)
        {
            _categoryDomainService.CheckCategoryName(command.Name);
            var category = new CategoryRoot(command.Id, command.Name,
                command.Description,
                _repository.AsQuery().OfType<CategoryRoot>().Count(),
                new CategoryImage(command.CategoryImage.MainCatImage, command.CategoryImage.FullMainCatImage,
                    command.CategoryImage.TopPageCatImage))
            {
                SubCategories = new List<Category>()
            };
            _repository.Add(category);
            return Task.FromResult(new CreateCategoryRootCommandResponse());
        }
        public Task<UpdateCategoryRootCommandResponse> Handle(UpdateCategoryRootCommand command)
        {
            _categoryDomainService.CheckCategoryName(command.Id, command.Name);
            var categoryRoot = _repository.AsQuery().SingleOrDefault(p => p.Id == command.Id);
            if (categoryRoot == null)
            {
                throw new DomainException("دسته یافت نشد");
            }
            categoryRoot.Name = command.Name;
            categoryRoot.Description = command.Description;
            categoryRoot.CategoryImage = new CategoryImage(command.CategoryImage.MainCatImage, command.CategoryImage.FullMainCatImage, command.CategoryImage.TopPageCatImage);
            return Task.FromResult(new UpdateCategoryRootCommandResponse());
        }
        public Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand command)
        {
            _categoryDomainService.CheckCategoryName(command.Id, command.Name);
            var category = _repository.AsQuery().SingleOrDefault(p => p.Id == command.Id);
            if (category == null)
            {
                throw new DomainException("دسته یافت نشد");
            }
            category.Name = command.Name;
            category.Description = command.Description;
            return Task.FromResult(new UpdateCategoryCommandResponse());
        }
        public Task<ActiveCategoryCommandResponse> Handle(ActiveCategoryCommand command)
        {
            var category = _repository.Find(command.Id);
            if (category == null)
            {
                throw new DomainException("دسته بندی یافت نشد");
            }
            category.Active();
            return Task.FromResult(new ActiveCategoryCommandResponse());
        }
        public Task<DeActiveCategoryCommandResponse> Handle(DeActiveCategoryCommand command)
        {
            var category = _repository.Find(command.Id);
            if (category == null)
            {
                throw new DomainException("دسته بندی یافت نشد");
            }
            category.DeActive();
            return Task.FromResult(new DeActiveCategoryCommandResponse());
        }

        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand command)
        {
            var category = await _repository.FindAsync(command.Id);
            if (category == null)
            {
                throw new DomainException("دسته یافت نشد");
            }
            if (_productRepository.AsQuery().Any(p => p.Category.Id == category.Id))
            {
                throw new DomainException("این دسته دارای محصول می باشد و قادر به حذف آن نمی باشید");
            }
            _repository.Remove(category);
            return new DeleteCategoryCommandResponse();
        }

        public async Task<DeleteCategoryRootCommandResponse> Handle(DeleteCategoryRootCommand command)
        {
            var categoryRoot = await _repository.FindAsync(command.Id);
            if (categoryRoot == null)
            {
                throw new DomainException("دسته یافت نشد");
            }
            if (categoryRoot.SubCategories.Any())
            {
                throw new DomainException("این دسته بندی دارای زیر دسته بندی می باشد");
            }
            _repository.Remove(categoryRoot);
            return new DeleteCategoryRootCommandResponse();
        }

        public async Task<SortCategoryCommandResponse> Handle(SortSubCategoryCommand command)
        {
            var categoryRoot = await _repository.FindAsync(command.CaegoryRootId);
            if (categoryRoot == null)
            {
                throw new DomainException("دسته یافت نشد");
            }
            foreach (var item in command.CategoryOrders)
            {
                var subCategory = categoryRoot.SubCategories.SingleOrDefault(p => p.Id == item.Id);
                if (subCategory == null)
                {
                    throw new DomainException("زیر دسته بندی یافت نشد");
                }
                subCategory.Order = item.Order;
            }
            return new SortCategoryCommandResponse();
        }

        public async Task<SortCategoryRootCommandResponse> Handle(SortCategoryRootCommand command)
        {
            foreach (var item in command.CategoryOrders)
            {
                var categoryRoot = await _repository.FindAsync(item.Id);
                if (categoryRoot == null)
                {
                    throw new DomainException("دسته یافت نشد");
                }
                categoryRoot.Order = item.Order;
            }
            return new SortCategoryRootCommandResponse();
        }
    }
}