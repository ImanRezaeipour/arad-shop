using System;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Sliders.Aggregates
{
    public class Slider : AggregateRoot, IPassivable
    {
        protected Slider()
        {
        }
        public Slider(Guid id, string imageName, int order, SliderType sliderType)
        {
            Id = id;
            SliderType = sliderType;
            Order = order;
            ImageName = imageName;
            IsActive = false;
            SliderType = SliderType.Slider;
        }
        public string ImageName { get; set; }
        public bool IsActive { get; private set; }
        public SliderType SliderType { get; set; }
        public Guid AdditionalData { get; set; }
        public int Order { get;  set; }
        public void Active() => IsActive = true;
        public void DeActive() => IsActive = false;
        public override void Validate()
        {
        }
    }
}