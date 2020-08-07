using System.Configuration;

namespace Shopping.Infrastructure
{
    public static class ShoppingConfiguration
    {
        public static string IpgUrl = ConfigurationManager.AppSettings["IPGUrl"];
        public static string IpgCallBackUrl = ConfigurationManager.AppSettings["IpgCallBackUrl"];
    }
}