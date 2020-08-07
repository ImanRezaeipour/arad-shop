using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using Microsoft.Owin;
using Newtonsoft.Json;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.IosStore
{
    public class IosStoreController : ApiMobileControllerBase
    {
        public bool Get()
        {
            var ip = GetClientIp(Request);
            if (ip == null)
            {
                return false;
            }
            var country = GetUserCountryByIp(ip);
            if (country == "Iran" || country == null)
            {
                return false;
            }
            return true;
        }
        private string GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRi1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRi1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }
            return ipInfo.Country;
        }
        private string GetClientIp(HttpRequestMessage request)
        {
            const string OWIN_CONTEXT = "MS_OwinContext";
            if (request.Properties.ContainsKey(OWIN_CONTEXT))
            {
                if (request.Properties[OWIN_CONTEXT] is OwinContext owinContext)
                    return owinContext.Request.RemoteIpAddress;
            }
            return null;
        }
    }
    public class IpInfo
    {

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }
    }
}