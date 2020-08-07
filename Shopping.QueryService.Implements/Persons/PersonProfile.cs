using AutoMapper;
using PersianDate;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Entities;
using Shopping.DomainModel.Aggregates.Persons.Entities.CustomerEntities;
using Shopping.DomainModel.Aggregates.Persons.ValueObjects;
using Shopping.Infrastructure.Helper;
using Shopping.QueryModel.Implements;
using Shopping.QueryModel.Implements.Persons;
using Shopping.QueryModel.QueryModels.Persons.Abstract;
using Shopping.QueryModel.QueryModels.Persons.AppInfo;
using Shopping.QueryModel.QueryModels.Persons.Customer;
using Shopping.QueryModel.QueryModels.Persons.Shop;

namespace Shopping.QueryService.Implements.Persons
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<AppInfo, IAppInfoDto>();
            CreateMap<Person, IPersonDto>()
                .Include<Customer, ICustomerDto>()
                .Include<Shop, IShopDto>();

            CreateMap<Shop, IShopDto>()
                .ForMember(item => item.CreationTime, opt => opt.MapFrom(src => src.RegisterDate));
            CreateMap<Shop, ShopDto>()
                .ForMember(item => item.CreationTime, opt => opt.MapFrom(src => src.RegisterDate));
            CreateMap<Shop, IShopFullInfo>()
                .ForMember(item => item.CreationTime, opt => opt.MapFrom(src => src.RegisterDate));
            CreateMap<ImageDocuments, IImageDocumentsDto>();
            CreateMap<BankAccount, IBankAccountDto>();
            CreateMap<ShopAddress, IShopAddressDto>()
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => new PositionDto(src.Geography.ToPosition().Latitude,
                        src.Geography.ToPosition().Longitude)));



            CreateMap<Customer, ICustomerDto>()
                .ForMember(item => item.CreationTime, opt => opt.MapFrom(src => src.RegisterDate));
            CreateMap<Customer, ICustomerWithDefaultAddressDto>()
                .ForMember(item => item.CreationTime, opt => opt.MapFrom(src => src.RegisterDate))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToFa("yyyy/MM/dd")));

            CreateMap<Customer, ICustomerWithAddressesDto>()
                .ForMember(item => item.CreationTime, opt => opt.MapFrom(src => src.RegisterDate))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToFa("yyyy/MM/dd")));

            CreateMap<ShopAddress, IShopPositionDto>()
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => new PositionDto(src.Geography.ToPosition().Latitude,
                        src.Geography.ToPosition().Longitude)));

            CreateMap<Shop, IShopWithAddressDto>()
                .ForMember(item => item.CreationTime, opt => opt.MapFrom(src => src.RegisterDate));
            CreateMap<Shop, IShopPositionDto>()
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => new PositionDto(src.ShopAddress.Geography.ToPosition().Latitude,
                        src.ShopAddress.Geography.ToPosition().Longitude)));

            CreateMap<DefultCustomerAddress, IDefultCustomerAddressDto>()
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => new PositionDto(src.Geography.ToPosition().Latitude,
                        src.Geography.ToPosition().Longitude)));

            CreateMap<CustomerAddress, ICustomerAddressDto>()
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => new PositionDto(src.Geography.ToPosition().Latitude,
                        src.Geography.ToPosition().Longitude)));

            CreateMap<Shop, IShopWithLogDto>();
            CreateMap<ShopStatusLog, IShopStatusLogDto>();
        }
    }
}