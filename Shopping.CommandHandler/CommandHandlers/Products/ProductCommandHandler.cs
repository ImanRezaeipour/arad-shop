using System;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Products.Commands;
using Shopping.Commands.Commands.Products.Responses;
using Shopping.DomainModel.Aggregates.Brands.Aggregates;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.DomainModel.Aggregates.Products.Entities;
using Shopping.DomainModel.Aggregates.Products.ValueObjects;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Products
{
    public class ProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResponse>,
        ICommandHandler<UpdateProductCommand, UpdateProductCommandResponse>,
        ICommandHandler<DeleteProductCommand, DeleteProductCommandResponse>,
        ICommandHandler<ActiveProductCommand, ActiveProductCommandResponse>,
        ICommandHandler<DeActiveProductCommand, DeActiveProductCommandResponse>
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<CategoryBase> _categoryRepository;
        private readonly IRepository<Factor> _factorRepository;
        private readonly IRepository<OrderBase> _ordeRepository;
        private readonly IContext _context;
        public ProductCommandHandler(IRepository<Product> repository, IRepository<Brand> brandRepository, IRepository<CategoryBase> categoryRepository, IContext context, IRepository<Factor> factorRepository, IRepository<OrderBase> ordeRepository)
        {
            _repository = repository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _context = context;
            _factorRepository = factorRepository;
            _ordeRepository = ordeRepository;
        }
        public Task<CreateProductCommandResponse> Handle(CreateProductCommand command)
        {
            var brand = _brandRepository.Find(command.BrandId);

            if (brand == null)
            {
                throw new DomainException("برند وارد شده یافت نشد");
            }
            var category = _categoryRepository.Find(command.CategoryId);
            if (category == null)
            {
                throw new DomainException("دسته بندی وارد شده یافت نشد");
            }
            var product = new Product(command.Id, command.Name, command.ShortDescription, command.Description,
                command.Price, brand, category, command.MainImage,
                new FakeProductDiscount(command.ProductDiscount.From, command.ProductDiscount.To))
            {
                ProductImages = command.ProductImages
                    .Select(item => new ProductImage(Guid.NewGuid(), item.Url, item.Order)).ToList()
            };
            _repository.Add(product);
            _context.SaveChanges();
            return Task.FromResult(new CreateProductCommandResponse());
        }
        public Task<UpdateProductCommandResponse> Handle(UpdateProductCommand command)
        {
            var brand = _brandRepository.Find(command.BrandId);
            if (brand == null)
            {
                throw new DomainException("برند وارد شده یافت نشد");
            }
            var category = _categoryRepository.Find(command.CategoryId);
            if (category == null)
            {
                throw new DomainException("دسته بندی وارد شده یافت نشد");
            }
            var product = _repository.Find(command.Id);
            if (product == null)
            {
                throw new DomainException("محصول یافت نشد");
            }
            product.Category = category;
            product.Brand = brand;
            product.Description = command.Description;
            product.Name = command.Name;
            product.Price = command.Price;
            product.ShortDescription = command.ShortDescription;
            product.ProductImages.Clear();
            if (product.ProductImages.Count == 0)
                product.ProductImages =
                command.ProductImages.Select(p => new ProductImage(Guid.NewGuid(), p.Url, p.Order)).ToList();
            product.MainImage = command.MainImage;
            product.Discount = new FakeProductDiscount(command.ProductDiscount.From, command.ProductDiscount.To);
            _context.SaveChanges();
            return Task.FromResult(new UpdateProductCommandResponse());
        }
        public Task<ActiveProductCommandResponse> Handle(ActiveProductCommand command)
        {
            var product = _repository.Find(command.Id);
            if (product == null)
            {
                throw new DomainException("محصول یافت نشد");
            }
            product.Active();
            _context.SaveChanges();
            return Task.FromResult(new ActiveProductCommandResponse());
        }
        public Task<DeActiveProductCommandResponse> Handle(DeActiveProductCommand command)
        {
            var product = _repository.Find(command.Id);
            if (product == null)
            {
                throw new DomainException("محصول یافت نشد");
            }
            product.DeActive();
            _context.SaveChanges();
            return Task.FromResult(new DeActiveProductCommandResponse());
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand command)
        {
            var product = await _repository.FindAsync(command.Id);
            if (product == null)
            {
                throw new DomainException("محصول یافت نشد");
            }
            if (_factorRepository.AsQuery().Any(p => p.FactorRaws.Any(o => o.ProductId == product.Id)))
            {
                throw new DomainException("برای این محصول فاکتوری ایجاد شده و امکان حذف ان نمی باشد");
            }
            if (_ordeRepository.AsQuery().Any(p => p.OrderItems.Any(o => o.OrderProduct.ProductId == product.Id)))
            {
                throw new DomainException("برای این محصول سفارش ایجاد شده و امکان حذف ان نمی باشد");
            }
            product.ProductImages.Clear();
            _repository.Remove(product);
            return new DeleteProductCommandResponse();

        }
    }
}