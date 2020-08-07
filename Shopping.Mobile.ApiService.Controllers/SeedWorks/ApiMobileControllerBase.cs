using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using Shopping.Infrastructure.Core.CommandBus;

#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.Controllers.SeedWorks
{
    public abstract class ApiMobileControllerBase : ApiController
    {
        protected ICommandBus Bus;
        protected ApiMobileControllerBase(ICommandBus bus)
        {
            Bus = bus;
        }
        protected ApiMobileControllerBase()
        {
        }
        protected Guid UserId
        {
            get
            {
                if (RequestContext.Principal?.Identity is ClaimsIdentity s)
                {
                    var userId = s.Claims.FirstOrDefault(i => i.Type.Equals("UserId"));
                    if (userId != null)
                    {
                        return Guid.Parse(userId.Value);
                    }
                    throw new Exception("Invalid Token");
                }
                throw new Exception("Invalid Token");
            }
        }
        protected string MobileNumber
        {
            get
            {
                if (RequestContext.Principal?.Identity is ClaimsIdentity s)
                {
                    var mobileNumber = s.Claims.FirstOrDefault(i => i.Type.Equals("MobileNumber"));
                    if (mobileNumber != null)
                    {
                        return mobileNumber.Value;
                    }
                    throw new Exception("Invalid Token");
                }
                throw new Exception("Invalid Token");
            }
        }
        protected string DeviceId
        {
            get
            {
                if (RequestContext.Principal?.Identity is ClaimsIdentity s)
                {
                    var mobileNumber = s.Claims.FirstOrDefault(i => i.Type.Equals("DeviceId"));
                    if (mobileNumber != null)
                    {
                        return mobileNumber.Value;
                    }
                    throw new Exception("Invalid Token");
                }
                throw new Exception("Invalid Token");
            }
        }
    }
}