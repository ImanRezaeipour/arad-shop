using System.Linq;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.Marketers.Interfaces;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Marketers.Services
{
    public class MarketerDomainService : IMarketerDomainService
    {
        private readonly IRepository<Shop> _shopRepository;
        public MarketerDomainService(IRepository<Shop> shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public void CheckMarketerActive(Marketer marketer)
        {
            if (!marketer.IsActive)
            {
                throw new DomainException("بازاریاب غیرفعال می باشد");
            }
        }
        public void CheckMaxMarketerAllowedIsEnough(Marketer marketer)
        {
            var count = _shopRepository.AsQuery().Count(p => p.MarketerId == marketer.Id && !p.IsActive && p.ShopStatus == ShopStatus.Accept);
            if (count >= marketer.MaxMarketerAllowed)
            {
                throw new DomainException("حداکثر تعداد مجاز فروشگاه برای بازاریابی به اتمام رسیده است");
            }
        }
    }
}