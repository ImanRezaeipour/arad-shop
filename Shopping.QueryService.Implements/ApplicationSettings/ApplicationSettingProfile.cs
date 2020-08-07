using AutoMapper;
using Shopping.QueryModel.QueryModels.ApplicationSettings;

namespace Shopping.QueryService.Implements.ApplicationSettings
{
    public class ApplicationSettingProfile:Profile
    {
        public ApplicationSettingProfile()
        {
            CreateMap<DomainModel.Aggregates.ApplicationSettings.Aggregates.ApplicationSetting, IApplicationSettingDto>()
                .ForMember(dest=>dest.OrderExpierTime,opt=>opt.Ignore());
        }
    }
}