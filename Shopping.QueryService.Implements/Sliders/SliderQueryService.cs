using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Sliders.Aggregates;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.Implements.Slider;
using Shopping.QueryModel.QueryModels.Sliders;
using Shopping.QueryService.Interfaces.Sliders;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Sliders
{
    public class SliderQueryService : ISliderQueryService
    {
        private readonly IReadOnlyRepository<Slider, Guid> _repository;
        private readonly IReadOnlyRepository<DiscountBase, Guid> _discountRepository;
        public SliderQueryService(IReadOnlyRepository<Slider, Guid> repository, IReadOnlyRepository<DiscountBase, Guid> discountRepository)
        {
            _repository = repository;
            _discountRepository = discountRepository;
        }
        public IEnumerable<ISliderDto> GetAll()
        {
            var result = _repository.AsQuery().OrderBy(i => i.Order).ToList();
            return result.Select(p => p.ToDto());
        }
        public IEnumerable<ISliderDiscountDto> GetActiveSliders()
        {
            var result = _repository.AsQuery().Where(p => p.IsActive)
                .OrderBy(p => p.Order).ToList();
            return result.Select(p => p.ToDiscountDto());
        }
        public IEnumerable<ISliderDiscountDto> GetDiscountsSlider()
        {
            var sliders = _repository.AsQuery().Where(p => p.SliderType == SliderType.Disount99)
                .OrderBy(p => p.Order).ToList();
            var result = sliders.Select(p => new SliderDiscount
            {
                Id = p.Id,
                ImageName = p.ImageName,
                IsActive = p.IsActive,
                SliderType = p.SliderType,
                AdditionalData = p.AdditionalData,
                Order = p.Order
            }).ToList();
            foreach (var item in result)
            {
                var discount = _discountRepository.AsQuery().SingleOrDefault(p => p.Id == item.AdditionalData);
                if (discount != null)
                {
                    item.DiscountTitle = discount.Title;
                }
            }
            return result;
        }
    }
}