using AutoMapper;
using Shopping.DomainModel.Aggregates.FakeIos.Orders;
using Shopping.QueryModel.QueryModels.FakeIos.Orders;

namespace Shopping.QueryService.Implements.FakeIos
{
    public class FakeIosProfile : Profile
    {
        public FakeIosProfile()
        {
            CreateMap<FakeOrderIos, IFakeOrderIosDto>()
                .ForMember(dest=>dest.CreationTime,opt=>opt.MapFrom(src=>src.CreationTime.ToString("MM/dd/yyyy h:mm tt")));
            CreateMap<FakeOrderIosItem, IFakeOrderIosItemDto>();
        }
    }
}