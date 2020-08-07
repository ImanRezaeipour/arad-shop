using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Brands.Commands;
using Shopping.Commands.Commands.Brands.Responses;
using Shopping.DomainModel.Aggregates.Brands.Aggregates;
using Shopping.DomainModel.Aggregates.Brands.Interfaces;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Brands
{
    public class BrandCommandHandler :
        ICommandHandler<CreateBrandCommand, CreateBrandCommandResponse>
        , ICommandHandler<UpdateBrandCommand, UpdateBrandCommandResponse>
        , ICommandHandler<ActiveBrandCommand, ActiveBrandCommandResponse>
        , ICommandHandler<DeActiveBrandCommand, DeActiveBrandCommandResponse>
        ,ICommandHandler<DeleteBrandCommand, DeleteBrandCommandResponse>
    {
        private readonly IBrandDomainService _brandDomainService;
        private readonly IRepository<Brand> _repository;
        private readonly IRepository<Product> _productRepository;
        public BrandCommandHandler(IBrandDomainService brandDomainService,
            IRepository<Brand> repository,
            IRepository<Product> productRepository)
        {
            _brandDomainService = brandDomainService;
            _repository = repository;
            _productRepository = productRepository;
        }
        public Task<CreateBrandCommandResponse> Handle(CreateBrandCommand command)
        {
            _brandDomainService.CheckBrandName(command.Name, command.LatinName);
            var brand = new Brand(command.Id, command.Name, command.LatinName);
            _repository.Add(brand);
            return Task.FromResult(new CreateBrandCommandResponse());
        }
        public Task<UpdateBrandCommandResponse> Handle(UpdateBrandCommand command)
        {
            _brandDomainService.CheckEditedBrandName(command.Id, command.Name, command.LatinName);
            var brand = _repository.Find(command.Id);
            if (brand == null)
            {
                throw new DomainException("برند یافت نشد");
            }
            brand.Name = command.Name;
            brand.LatinName = command.LatinName;
            return Task.FromResult(new UpdateBrandCommandResponse());
        }
        public Task<ActiveBrandCommandResponse> Handle(ActiveBrandCommand command)
        {
            var brand = _repository.Find(command.Id);
            if (brand == null)
            {
                throw new DomainException("برند یافت نشد");
            }
            brand.Active();
            return Task.FromResult(new ActiveBrandCommandResponse());
        }
        public Task<DeActiveBrandCommandResponse> Handle(DeActiveBrandCommand command)
        {
            var brand = _repository.Find(command.Id);
            if (brand == null)
            {
                throw new DomainException("برند یافت نشد");
            }
            brand.DeActive();
            return Task.FromResult(new DeActiveBrandCommandResponse());
        }

        public async Task<DeleteBrandCommandResponse> Handle(DeleteBrandCommand command)
        {
            var brand =await _repository.FindAsync(command.Id);
            if (brand == null)
            {
                throw new DomainException("برند یافت نشد");
            }
            if (_productRepository.AsQuery().Any(p=>p.Brand.Id==command.Id))
            {
                    throw  new DomainException("این برند داای محصول می باشد و قادر  به حذف ان نمی باشید");
            }
            _repository.Remove(brand);
            return new DeleteBrandCommandResponse(); 
        }
    }
}