using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected ICommandBus Bus;
        protected ApiControllerBase(ICommandBus bus)
        {
            Bus = bus;
        }
        protected ApiControllerBase()
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
        protected string FirstName
        {
            get
            {
                if (RequestContext.Principal?.Identity is ClaimsIdentity s)
                {
                    var firstName = s.Claims.FirstOrDefault(i => i.Type.Equals("FirstName"));
                    if (firstName != null)
                    {
                        return firstName.Value;
                    }
                    throw new Exception("Invalid Token");
                }
                throw new Exception("Invalid Token");
            }
        }
        protected string LastName
        {
            get
            {
                if (RequestContext.Principal?.Identity is ClaimsIdentity s)
                {
                    var lastName = s.Claims.FirstOrDefault(i => i.Type.Equals("LastName"));
                    if (lastName != null)
                    {
                        return lastName.Value;
                    }
                    throw new Exception("Invalid Token");
                }
                throw new Exception("Invalid Token");
            }
        }
    }
}