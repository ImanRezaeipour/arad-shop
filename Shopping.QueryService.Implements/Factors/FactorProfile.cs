using System;
using AutoMapper;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Entities;
using Shopping.DomainModel.Aggregates.Factors.Entities.ShipmentState;
using Shopping.DomainModel.Aggregates.Factors.Entities.State;
using Shopping.DomainModel.Aggregates.Factors.ValueObjects;
using Shopping.Infrastructure.Helper;
using Shopping.QueryModel.Implements;
using Shopping.QueryModel.QueryModels.Factors;
using Shopping.QueryModel.QueryModels.Factors.FactorStates;
using Shopping.QueryModel.QueryModels.Factors.ShipmentStates;

namespace Shopping.QueryService.Implements.Factors
{
    public class FactorProfile : Profile
    {
        public FactorProfile()
        {
            CreateMap<Factor, IFactorWithCustomerDto>()
                .ForMember(des => des.FactoRawCount, opt => opt.MapFrom(src => src.FactorRaws.Count))
                .ForMember(des => des.ShipmentTimeLeft, opt => opt.ResolveUsing(CalcShipmentTimeLeft))
                .ForMember(dest=>dest.IsExpired,opt=>opt.Ignore());

            CreateMap<Factor, IFactorWithShopDto>()
                .ForMember(des => des.FactoRawCount, opt => opt.MapFrom(src => src.FactorRaws.Count))
                .ForMember(des => des.ShipmentTimeLeft, opt => opt.ResolveUsing(CalcShipmentTimeLeft))
                .ForMember(dest => dest.IsExpired, opt => opt.Ignore());


            CreateMap<Factor, IFactorDto>()
                .ForMember(des => des.ShipmentTimeLeft, opt => opt.ResolveUsing(CalcShipmentTimeLeft))
                .ForMember(des => des.FactoRawCount, opt => opt.MapFrom(src => src.FactorRaws.Count))
                .ForMember(dest => dest.IsExpired, opt => opt.Ignore());


            CreateMap<Factor, IFactorFullInfoDto>()
                .ForMember(des => des.ShipmentTimeLeft, opt => opt.ResolveUsing(CalcShipmentTimeLeft))
                .ForMember(des => des.FactoRawCount, opt => opt.MapFrom(src => src.FactorRaws.Count))
                .ForMember(dest => dest.IpgResult, opt => opt.MapFrom(src => src.FactorStateBase))
                .ForMember(dest => dest.IsExpired, opt => opt.Ignore());


            CreateMap<FactorRaw, IFactorRawDto>()
                .ForMember(dest => dest.HaveDiscount, opt => opt.MapFrom(src => src.Discount != null));

            CreateMap<FactorAddress, IFactorAddressDto>()
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => new PositionDto(src.Geography.ToPosition().Latitude,
                        src.Geography.ToPosition().Longitude)));

            CreateMap<FactorStateBase, IFactorStateBaseDto>()
                .Include<PendingFactorState, IPendingFactorStateDto>()
                .Include<PaidFactorState, IPaidFactorStateDto>()
                .Include<FailedFactorState, IFailedFactorStateDto>();

            CreateMap<PendingFactorState, IPendingFactorStateDto>();
            CreateMap<PaidFactorState, IPaidFactorStateDto>();
            CreateMap<FailedFactorState, IFailedFactorStateDto>();

            CreateMap<ShipmentStateBase, IShipmentStateBaseDto>()
                .Include<ReverseShipmentState, IReverseShipmentStateDto>()
                .Include<PendingShipmentState, IPendingShipmentStateDto>()
                .Include<DeliveryShipmentState, IDeliveryShipmentStateDto>()
                .Include<SendShipmentState, ISendShipmentStateDto>();

            CreateMap<ReverseShipmentState, IReverseShipmentStateDto>();
            CreateMap<PendingShipmentState, IPendingShipmentStateDto>();
            CreateMap<DeliveryShipmentState, IDeliveryShipmentStateDto>();
            CreateMap<SendShipmentState, ISendShipmentStateDto>();
        }
        
        private int CalcShipmentTimeLeft(Factor factor)
        {
            if (factor.FactorStateBase is PaidFactorState paidFactorState)
            {
                var expireShipmentTime = paidFactorState.PayTime.AddMinutes(factor.ShippingTime);
                if (DateTime.Now>= expireShipmentTime)
                {
                    return 0;
                }
                var result= 
                    (int)expireShipmentTime.Subtract(DateTime.Now).TotalMinutes >= 0 ?
                    (int)expireShipmentTime.Subtract(DateTime.Now).TotalMinutes : 0;
                return result;
            }
            return factor.ShippingTime;
        }
    }
}