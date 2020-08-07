using Shopping.DomainModel.Aggregates.Discounts.Aggregates;
using Shopping.DomainModel.Aggregates.Discounts.Events;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Discounts.EventHandlers
{
    public class PercentDiscountEventHandler:IDomainEventHandler<AddDiscountSellEvent>
    {
        private readonly IRepository<PercentDiscount> _repository;
        public PercentDiscountEventHandler(IRepository<PercentDiscount> repository)
        {
            _repository = repository;
        }
        public void Handle(AddDiscountSellEvent @event)  
        {
            var percentDiscount = _repository.Find(@event.DiscountId);
            if (percentDiscount==null)
            {
                throw new DomainException("تخفیف یافت نشد");
            }
            //percentDiscount.Sells.Add(new DiscountSell(Guid.NewGuid(), @event.Product, @event.Customer,
            //    @event.DiscountSellType, @event.ShopDebitPrice, @event.FinancialBenefit, @event.Order));
        }
    }
}