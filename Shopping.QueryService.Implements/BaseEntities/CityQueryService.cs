using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.DomainModel.Aggregates.BaseEntities.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.QueryModel.Implements.BaseEntities;
using Shopping.QueryModel.QueryModels.BaseEntities;
using Shopping.QueryService.Interfaces.BaseEntities;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.BaseEntities
{
    public class CityQueryService : ICityQueryService
    {
        private readonly IReadOnlyRepository<Province, Guid> _repository;
        private readonly IReadOnlyRepository<City, Guid> _cityRepository;
        public CityQueryService(IReadOnlyRepository<Province, Guid> repository, IReadOnlyRepository<City, Guid> cityRepository)
        {
            _repository = repository;
            _cityRepository = cityRepository;
        }
        public IList<IProvinceDto> GetAll()
        {
            var result = _repository.AsQuery().ToList();
            return result.Select(p => p.ToDto()).ToList();
        }

        public IList<ICityWithoutZoneDto> GetAllCityWithoutZoneDtosByProvinecId(Guid provinecId)
        {
            var result = _cityRepository.AsQuery().Where(p => p.Province.Id == provinecId && p.IsActive).ToList();
            return result.Select(p => p.ToCityWithoutZoneDto()).ToList();

        }

        public IList<IProvinceWithoutCity> GetAllProvinecWithoutCities()
        {
            var result = _repository.AsQuery().ToList();
            return result.Select(p => p.ToProvinecWithoutCityDto()).ToList();
        }

        public IProvinceDto GetById(Guid id)
        {
            var result = _repository.AsQuery().SingleOrDefault(p => p.Id == id);
            return result.ToDto();
        }

        public IEnumerable<IZoneDto> GetZoneByCityId(Guid cityId)
        {
            var city = _cityRepository.AsQuery().SingleOrDefault(p => p.Id == cityId && p.Zones.Any(z => z.IsActive));
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }
            var result = city.Zones.Select(p => new ZoneDto
            {
                Id = p.Id,
                IsActive = p.IsActive,
                Title = p.Title
            }).ToList();
            return result.Where(p=>p.IsActive);
        }
    }
}