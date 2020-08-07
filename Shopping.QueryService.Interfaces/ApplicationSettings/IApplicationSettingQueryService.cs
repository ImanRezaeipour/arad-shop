using Shopping.QueryModel.QueryModels.ApplicationSettings;

namespace Shopping.QueryService.Interfaces.ApplicationSettings
{
    public interface IApplicationSettingQueryService
    {
        IApplicationSettingDto GetApplicationSetting();
    }
}