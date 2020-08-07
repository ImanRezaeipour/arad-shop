using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.BaseEntities.Commands;
using Shopping.Commands.Commands.BaseEntities.Responses;
using Shopping.DomainModel.Aggregates.BaseEntities.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.BaseEntities
{
    public class BaseEntitiesCommandHandler : ICommandHandler<ActiveCityCommand, ActiveCityCommandResponse>
        , ICommandHandler<ActiveZoneCommand, ActiveZoneCommandResponse>
        , ICommandHandler<CreateCityCommand, CreateCityCommandResponse>
        , ICommandHandler<CreateProvinceCommand, CreateProvinceCommandResponse>
        , ICommandHandler<CreateZoneCommand, CreateZoneCommandResponse>
        , ICommandHandler<DeActiveCityCommand, DeActiveCityCommandResponse>
        , ICommandHandler<DeActiveZoneCommand, DeActiveZoneCommandResponse>
        , ICommandHandler<UpdateCityCommand, UpdateCityCommandResponse>
        , ICommandHandler<UpdateProvinceCommand, UpdateProvinceCommandResponse>
        , ICommandHandler<UpdateZoneCommand, UpdateZoneCommandResponse>

    {
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Province> _provinceRepository;
        public BaseEntitiesCommandHandler(IRepository<City> cityRepository, IRepository<Province> provinceRepository)
        {
            _cityRepository = cityRepository;
            _provinceRepository = provinceRepository;
        }

        public async Task<ActiveCityCommandResponse> Handle(ActiveCityCommand command)
        {
            var city = await _cityRepository.FindAsync(command.Id);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }
            city.Active();
            return new ActiveCityCommandResponse();
        }
        public async Task<CreateCityCommandResponse> Handle(CreateCityCommand command)
        {
            var province = _provinceRepository.AsQuery().SingleOrDefault(p => p.Id == command.ProvinceId);
            if (province == null)
            {
                throw new DomainException("استان یافت نشد");
            }

            var isExist = await _cityRepository.AsQuery().AnyAsync(item => item.Code == command.Code);
            if (isExist)
            {
                throw new DomainException("شهر با این کد قبلا ثبت شده است");
            }
            var city = new City(Guid.NewGuid(), command.Code, command.CityName, province);
            _cityRepository.Add(city);
            return new CreateCityCommandResponse();
        }
        public async Task<DeActiveCityCommandResponse> Handle(DeActiveCityCommand command)
        {
            var city = await _cityRepository.FindAsync(command.Id);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }
            city.DeActive();
            return new DeActiveCityCommandResponse();
        }
        public async Task<UpdateCityCommandResponse> Handle(UpdateCityCommand command)
        {
            var isExist = await _cityRepository.AsQuery()
                .AnyAsync(item => item.Code == command.Code && item.Id != command.Id);
            if (isExist)
            {
                throw new DomainException("شهر با این کد قبلا ثبت شده است");
            }
            var city = await _cityRepository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.Id);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }
            city.CityName = command.CityName;
            city.Code = command.Code;
            return new UpdateCityCommandResponse();
        }


        public async Task<ActiveZoneCommandResponse> Handle(ActiveZoneCommand command)
        {
            var city = await _cityRepository.FindAsync(command.CityId);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }
            var zone = city.Zones.SingleOrDefault(p => p.Id == command.Id);
            if (zone == null)
            {
                throw new DomainException("منطقه یافت نشد");
            }
            zone.Active();
            return new ActiveZoneCommandResponse();
        }
        public async Task<UpdateZoneCommandResponse> Handle(UpdateZoneCommand command)
        {
            var city = await _cityRepository.FindAsync(command.CityId);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }
            var zone = city.Zones.SingleOrDefault(p => p.Id == command.Id);
            if (zone == null)
            {
                throw new DomainException("منطقه یافت نشد");
            }
            zone.Title = command.Title;
            return new UpdateZoneCommandResponse();
        }
        public async Task<CreateZoneCommandResponse> Handle(CreateZoneCommand command)
        {
            var city = await _cityRepository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.CityId);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }
            var zone = new Zone(command.Title);
            city.Zones.Add(zone);
            return new CreateZoneCommandResponse();
        }
        public async Task<DeActiveZoneCommandResponse> Handle(DeActiveZoneCommand command)
        {
            var city = await _cityRepository.FindAsync(command.CityId);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }
            var zone = city.Zones.SingleOrDefault(p => p.Id == command.Id);
            if (zone == null)
            {
                throw new DomainException("منطقه یافت نشد");
            }
            zone.DeActive();
            return new DeActiveZoneCommandResponse();
        }

        public async Task<CreateProvinceCommandResponse> Handle(CreateProvinceCommand command)
        {
            var isExist = await _provinceRepository.AsQuery().AnyAsync(item => item.Code == command.Code);
            if (isExist)
            {
                throw new DomainException("استان با این کد قبلا ثبت شده است");
            }
            var province = new Province(Guid.NewGuid(), command.Code, command.Name);
            _provinceRepository.Add(province);
            return new CreateProvinceCommandResponse();
        }
        public async Task<UpdateProvinceCommandResponse> Handle(UpdateProvinceCommand command)
        {
            var province = await _provinceRepository.FindAsync(command.Id);
            if (province == null)
            {
                throw new DomainException("استان یافت نشد");
            }
            var isExist = await _provinceRepository.AsQuery()
                .AnyAsync(item => item.Code == command.Code && item.Id != command.Id);
            if (isExist)
            {
                throw new DomainException("استان با این کد قبلا ثبت شده است");
            }
            province.Name = command.Name;
            province.Code = command.Code;
            return new UpdateProvinceCommandResponse();
        }
    }
}