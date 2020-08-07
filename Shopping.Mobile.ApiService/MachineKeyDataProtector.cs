using System.Web.Security;
using Microsoft.Owin.Security.DataProtection;
#pragma warning disable 1591

namespace Shopping.Mobile.ApiService
{
    public class MachineKeyDataProtector : IDataProtector
    {
        private readonly string[] _purposes;
        public MachineKeyDataProtector(params string[] purposes)
        {
            _purposes = purposes;
        } 
        public byte[] Protect(byte[] userData)
        {
            return MachineKey.Protect(userData, _purposes);
        }
        public byte[] Unprotect(byte[] protectedData)
        {
            return MachineKey.Unprotect(protectedData, _purposes);
        }
    }
}