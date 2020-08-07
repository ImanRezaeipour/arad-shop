using System;
using Microsoft.Owin.Hosting;

namespace Shopping.ApiService
{
    public class ApiStart
    {
        readonly string _baseAddress = ApiSettings.Default.HostApiAddress;
        private IDisposable _server = null;
        public void Start()
        {
            _server = WebApp.Start<Startup>(url: _baseAddress);
            Console.WriteLine("Listening on: " + _baseAddress);
        }
        public void Stop()
        {
            _server?.Dispose();
        }
    }

}
