using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.Implements.Dashboards;
using Shopping.QueryModel.QueryModels.Dashboards;
using Shopping.QueryService.Interfaces.Dashboards;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Dashboards
{
    public class DashboardCountQueryService : IDashboardCountQueryService
    {
        private readonly IReadOnlyRepository<Person, Guid> _personRepository;
        private readonly IReadOnlyRepository<Factor, long> _factorRepository;
        public DashboardCountQueryService(IReadOnlyRepository<Person, Guid> personRepository, IReadOnlyRepository<Factor, long> factorRepository)
        {
            _personRepository = personRepository;
            _factorRepository = factorRepository;
        }
        public IDashboardCountDto GetAppInfoCountByCityId(Guid cityId)
        {
            var dashboardCount = new DashboardCountDto
            {
                CustomerActiveCount = _personRepository.AsQuery().OfType<Customer>().Count(p => p.IsActive && p.DefultCustomerAddress.CityId != null && p.DefultCustomerAddress.CityId == cityId),
                CustomerDeActiveCount = _personRepository.AsQuery().OfType<Customer>().Count(p => !p.IsActive && p.DefultCustomerAddress.CityId != null && p.DefultCustomerAddress.CityId == cityId),
                ShopActiveCount = _personRepository.AsQuery().OfType<Shop>().Count(p => p.IsActive && p.ShopAddress.CityId == cityId),
                ShopDeActiveCount = _personRepository.AsQuery().OfType<Shop>().Count(p => p.IsActive == false && p.ShopAddress.CityId == cityId),
                FactorPaidCount = _factorRepository.AsQuery().Count(p => p.FactorState == FactorState.Paid && p.FactorAddress.CityId == cityId),
                FactorPendingCount = _factorRepository.AsQuery().Count(p => p.FactorState != FactorState.Paid && p.FactorAddress.CityId == cityId)
            };
            return dashboardCount;
        }
    }
}