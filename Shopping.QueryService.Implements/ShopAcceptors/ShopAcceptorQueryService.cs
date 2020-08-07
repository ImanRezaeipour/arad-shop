using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.ShopAcceptors.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.QueryModel.Implements.ShopAcceptors;
using Shopping.QueryModel.QueryModels.ShopAcceptors;
using Shopping.QueryService.Interfaces.ShopAcceptors;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.ShopAcceptors
{
    public class ShopAcceptorQueryService : IShopAcceptorQueryService
    {
        private readonly IReadOnlyRepository<ShopAcceptor, Guid> _repository;

        public ShopAcceptorQueryService(IReadOnlyRepository<ShopAcceptor, Guid> repository)
        {
            _repository = repository;
        }
        public IQueryable<IShopAcceptorsDto> GetAll()
        {
            var pastDate = DateTime.Today.AddDays(-15);
            var shopAcceptor = _repository.AsQuery().Where(item => item.CreationTime >= pastDate);
            var result = shopAcceptor.Select(p => new ShopAcceptorsDto
            {
                id = p.Id,
                firstName = p.FirstName,
                lastName = p.LastName,
                phoneNumber = p.PhoneNumber,
                mobileNumber = p.MobileNumber,
                shopName = p.ShopName,
                creationTime = p.CreationTime,
                shopAcceptorStatus = p.ShopAcceptorStatus
            });
            return result;
        }

        public IShopAcceptorsWithAddressDto GetById(Guid id)
        {
            var result = _repository.AsQuery().SingleOrDefault(p => p.Id == id);
            if (result == null)
            {
                throw new DomainException("فرم درخواست فروشگاه یافت نشد");
            }
            return result.ToDto();
        }
    }
}