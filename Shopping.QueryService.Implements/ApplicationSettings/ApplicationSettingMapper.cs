using AutoMapper;
using Shopping.QueryModel.QueryModels.ApplicationSettings;

namespace Shopping.QueryService.Implements.ApplicationSettings
{
    public static class ApplicationSettingMapper
    {
        public static IApplicationSettingDto ToDto(this DomainModel.Aggregates.ApplicationSettings.Aggregates.ApplicationSetting src)
        {
            return Mapper.Map<IApplicationSettingDto>(src);
        }
    }
}