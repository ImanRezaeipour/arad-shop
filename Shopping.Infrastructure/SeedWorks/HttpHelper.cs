using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Shopping.Infrastructure.SeedWorks
{
    public class HttpHelper : IDisposable
    {
        private readonly HttpClient _httpClient;
        public static HttpHelper Create(string apiBaseAddress)
        {
            return new HttpHelper(apiBaseAddress);
        }
        public HttpHelper(string apiBaseAddress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiBaseAddress),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }
       
        public T Post<T>(string controllerName, object obj)
        {
            string objStringify = JsonConvert.SerializeObject(obj);
            HttpContent content = new StringContent(objStringify, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(controllerName, content).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpException((int)response.StatusCode, $"خطا در سرویس {controllerName}");
            }
            var resultString = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<T>(resultString);
            return result;
        }
        public T Put<T>(string controllerName, object obj)
        {
            string objStringify = JsonConvert.SerializeObject(obj);
            HttpContent content = new StringContent(objStringify, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PutAsync(controllerName, content).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpException((int)response.StatusCode, $"خطا در سرویس {controllerName}");
            }
            var resultString = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<T>(resultString);
            return result;
        }
        public T Get<T>(string controllerName, Dictionary<string, string> urlParameters)
        {
            string parameters = BuildURLParametersString(urlParameters);
            var response = _httpClient.GetAsync(controllerName + parameters).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpException((int)response.StatusCode, $"خطا در سرویس {controllerName}");
            }
            var resultString = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<T>(resultString);
            return result;
        }
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
        private string BuildURLParametersString(Dictionary<string, string> parameters)
        {
            string query = "?";
            foreach (var pair in parameters)
                query += $"{pair.Key}={pair.Value}&";
            return query;
        }
    }
}