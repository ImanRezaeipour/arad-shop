using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using Microsoft.Owin;
using Newtonsoft.Json;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.IosStore
{
    public class IosStoreTestController : ApiMobileControllerBase
    {
        public Temp Get()
        {
            var ip = GetClientIp(Request);
            return new Temp
            {
                Ip = ip,
                IpInfo = GetUserCountryByIp(ip)
            };
        }
        private string GetClientIp(HttpRequestMessage request)
        {
            const string OWIN_CONTEXT = "MS_OwinContext";

            if (request.Properties.ContainsKey(OWIN_CONTEXT))
            {
                if (request.Properties[OWIN_CONTEXT] is OwinContext owinContext)
                    return owinContext.Request.RemoteIpAddress;
            }
            return "";
        }
        private IpInfo GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRi1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRi1.EnglishName;
                return ipInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class Temp
    {
        public IpInfo IpInfo { get; set; }
        public string Ip { get; set; }
    }
}