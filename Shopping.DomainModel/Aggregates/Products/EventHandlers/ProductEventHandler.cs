using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.DomainModel.Aggregates.Products.Entities.ProductDiscount;
using Shopping.DomainModel.Aggregates.Products.Events;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Products.EventHandlers
{
    public class ProductEventHandler : IDomainEventHandler<CreatePercentDiscountEvent>
        , IDomainEventHandler<UpdatePercentDiscountEvent>
    {
        private readonly IRepository<Product> _repository;
        public ProductEventHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public void Handle(CreatePercentDiscountEvent @event)
        {
            var product = _repository.AsQuery().SingleOrDefault(p => p.Id == @event.ProductId);
            if (product == null) return;
            {
                var percentDisCount = new ProductPercentDiscount(Guid.NewGuid(), @event.DiscountId, @event.Title, @event.UserInfo,
                    @event.FromDate, @event.ToDate, @event.DiscountType, @event.Percent
                    , @event.FromTime, @event.ToTime);
                product.ProductDiscount = percentDisCount;
            }
        }
        public void Handle(UpdatePercentDiscountEvent @event)
        {
            var products = _repository.AsQuery().Where(item => item.ProductDiscount != null &&
                                                               item.ProductDiscount is ProductPercentDiscount &&
                                                               item.ProductDiscount.DiscountId == @event.DiscountId)
                                                        .ToList();
            foreach (var product in products)
            {
                var discount = product.ProductDiscount as ProductPercentDiscount;
                if (discount != null)
                {
                    discount.FromDate = @event.FromDate;
                    discount.ToDate = @event.ToDate;
                    discount.Title = @event.Title;
                }
            }
        }
    }
}