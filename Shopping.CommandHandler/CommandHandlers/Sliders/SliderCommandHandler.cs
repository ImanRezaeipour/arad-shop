using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Sliders.Commands;
using Shopping.Commands.Commands.Sliders.Responses;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Sliders.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Sliders
{
    public class SliderCommandHandler : ICommandHandler<DeleteSlideCommand, DeleteSlideCommandResponse>
        , ICommandHandler<CreateSliderCommand, CreateSliderCommandResponse>
        , ICommandHandler<ActiveSliderCommand, ActivationSliderCommandResponse>
        , ICommandHandler<DeActiveSliderCommand, ActivationSliderCommandResponse>
        , ICommandHandler<AddSliderToDiscountCommand, AddSliderToDiscountCommandResponse>
        , ICommandHandler<ChangeTypeSliderToDiscountCommand, ChangeTypeSliderToDiscountCommandResponse>
        , ICommandHandler<SortOrderSliderCommand, SortOrderSliderCommandResponse>
    {
        private readonly IRepository<Slider> _repository;
        private readonly IRepository<DiscountBase> _discountRepository;
        public SliderCommandHandler(IRepository<Slider> repository, IRepository<DiscountBase> discountRepository)
        {
            _repository = repository;
            _discountRepository = discountRepository;
        }

        public async Task<DeleteSlideCommandResponse> Handle(DeleteSlideCommand command)
        {
            var slider = await _repository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.Id);
            if (slider == null)
            {
                throw new DomainException("اسلایدر یافت نشد");
            }
            _repository.Remove(slider);
            return new DeleteSlideCommandResponse();
        }

        public Task<CreateSliderCommandResponse> Handle(CreateSliderCommand command)
        {
            var slider = new Slider(Guid.NewGuid(), command.ImageName, _repository.AsQuery().Count(), command.SliderType);
            _repository.Add(slider);
            return Task.FromResult(new CreateSliderCommandResponse());
        }


        public async Task<ActivationSliderCommandResponse> Handle(ActiveSliderCommand command)
        {
            var slider = await _repository.FindAsync(command.Id);
            slider.Active();
            return new ActivationSliderCommandResponse();
        }
        public async Task<ActivationSliderCommandResponse> Handle(DeActiveSliderCommand command)
        {
            var slider = await _repository.FindAsync(command.Id);
            slider.DeActive();
            return new ActivationSliderCommandResponse();
        }

        public async Task<AddSliderToDiscountCommandResponse> Handle(AddSliderToDiscountCommand command)
        {
            var slider = await _repository.AsQuery().SingleOrDefaultAsync(p => p.ImageName == command.ImageName);
            if (slider == null)
            {
                throw new DomainException(" اسلایدر یافت نشد");
            }

            var disCount = await _discountRepository.FindAsync(command.DiscountId);
            if (disCount == null)
            {
                throw new DomainException("تخفیف موجود نیست");
            }
            slider.AdditionalData = command.DiscountId;
            slider.SliderType = command.SliderType;
            return new AddSliderToDiscountCommandResponse();
        }

        public async Task<ChangeTypeSliderToDiscountCommandResponse> Handle(ChangeTypeSliderToDiscountCommand command)
        {
            var slider = await _repository.AsQuery().SingleOrDefaultAsync(p => p.ImageName == command.ImageName);
            if (slider == null)
            {
                throw new DomainException(" اسلایدر یافت نشد");
            }
            slider.SliderType = command.SliderType;
            return new ChangeTypeSliderToDiscountCommandResponse();
        }

        public async Task<SortOrderSliderCommandResponse> Handle(SortOrderSliderCommand command)
        {
            foreach (var sliderSortItem in command.Sliders)
            {
                var slider = await _repository.FindAsync(sliderSortItem.Id);
                if (slider == null)
                {
                    throw new DomainException("اسلایدر یافت نشد");
                }
                slider.Order = sliderSortItem.Order;
            }
            return new SortOrderSliderCommandResponse();
        }
    }
}