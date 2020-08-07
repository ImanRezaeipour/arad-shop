using Microsoft.Owin.Security.OAuth;

#pragma warning disable 1591

namespace Shopping.ApiService
{
    public static class MachineKeyDataProtectorHelper
    {
        public static MachineKeyDataProtector New()
        {
            return new MachineKeyDataProtector(typeof(OAuthBearerAuthenticationMiddleware).Namespace, "Access_Token", "v1");
        }
    }
}