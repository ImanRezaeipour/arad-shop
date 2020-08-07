using AutoMapper;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Entities;
using Shopping.DomainModel.Aggregates.Persons.Entities.CustomerEntities;
using Shopping.DomainModel.Aggregates.Persons.ValueObjects;
using Shopping.QueryModel.QueryModels.Persons.AppInfo;
using Shopping.QueryModel.QueryModels.Persons.Customer;
using Shopping.QueryModel.QueryModels.Persons.Shop;

namespace Shopping.QueryService.Implements.Persons
{
    public static class PersonMapper
    {
        public static ICustomerDto ToDto(this Customer src)
        {
            return Mapper.Map<ICustomerDto>(src);
        }
        public static IAppInfoDto ToDto(this AppInfo src)
        {
            return Mapper.Map<IAppInfoDto>(src);
        }
        public static ICustomerWithDefaultAddressDto ToCustomerWithDefaultAddressDto(this Customer src)
        {
            return Mapper.Map<ICustomerWithDefaultAddressDto>(src);
        }
        public static ICustomerWithAddressesDto ToCustomerWithAddressesDto(this Customer src)
        {
            return Mapper.Map<ICustomerWithAddressesDto>(src);
        }
        public static ICustomerAddressDto ToDto(this CustomerAddress src)
        {
            return Mapper.Map<ICustomerAddressDto>(src);
        }
        public static IShopDto ToDto(this Shop src)
        {   
            return Mapper.Map<IShopDto>(src);
        }
        public static IShopFullInfo ToShopFullInfoDto(this Shop src)
        {
            return Mapper.Map<IShopFullInfo>(src);
        }

        public static IShopWithAddressDto ToShopWithAddressDto(this Shop src)
        {
            return Mapper.Map<IShopWithAddressDto>(src);
        }

        public static IShopAddressDto ToDto(this ShopAddress src)
        {
            return Mapper.Map<IShopAddressDto>(src);
        }
        public static IShopPositionDto ToShopPositionDto(this Shop src)
        {
            return Mapper.Map<IShopPositionDto>(src);
        }
        public static IShopWithLogDto ToShopLogDto(this Shop src)
        {
            return Mapper.Map<IShopWithLogDto>(src);
        }
    }
}