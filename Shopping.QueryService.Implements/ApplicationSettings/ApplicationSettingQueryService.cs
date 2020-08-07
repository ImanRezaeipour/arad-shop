using System;
using System.Linq;
using Shopping.QueryModel.QueryModels.ApplicationSettings;
using Shopping.QueryService.Interfaces.ApplicationSettings;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.ApplicationSettings
{
    public class ApplicationSettingQueryService: IApplicationSettingQueryService
    {
        private readonly IReadOnlyRepository<DomainModel.Aggregates.ApplicationSettings.Aggregates.ApplicationSetting,Guid>
            _repository;
        public ApplicationSettingQueryService(IReadOnlyRepository<DomainModel.Aggregates.ApplicationSettings.Aggregates.ApplicationSetting, Guid> repository)
        {
            _repository = repository;
        }
        public IApplicationSettingDto GetApplicationSetting()
        {
            var result = _repository.AsQuery().FirstOrDefault();
            return result.ToDto();
        }
    }
}