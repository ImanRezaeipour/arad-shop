using System;
using System.Threading.Tasks;
using Shopping.QueryModel.QueryModels.Persons.AppInfo;
using Shopping.Shared.Enums;

namespace Shopping.QueryService.Interfaces.Persons
{
    public interface IPersonQueryService
    {
        Task<IAppInfoDto> GetAppInfo(Guid userId, string authDeviceId,AppType appType);
    }
}