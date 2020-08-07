using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using NLog;
using Shopping.Infrastructure.Enum.Accounting;
using Shopping.Scheduler.Core.Contexts;
using Shopping.Scheduler.Core.Interfaces;
using Shopping.Scheduler.Core.Messages.Accounting.Details;

namespace Shopping.Scheduler.Core.Services
{
    public class RegisterPersonAccountingService : IRegisterPersonAccountingService
    {
        private readonly Logger _logger;
        private const string DetailServiceName = "Detail";
        private const string ShopClassCode = "60";
        private const string CustomerClassCode = "70";
        private readonly HttpClient _httpClient;
        public RegisterPersonAccountingService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["AccountingBaseUrl"]),
                Timeout = TimeSpan.FromSeconds(30)
            };
            _logger = LogManager.GetCurrentClassLogger();
        }
        public void RegisterCustomers()
        {
            using (var db = new ShoppingSchedulerContext())
            {
                var customers = db.Customers.Where(item => item.Accounting == null).ToList();
                foreach (var customer in customers)
                {
                    try
                    {
                        var createDetailRequest = new CreateDetailRequest
                        {
                            Code = 0,
                            RequestId = Guid.NewGuid(),
                            Name = $"{customer.FirstName} {customer.LastName}",
                            ClassCode = CustomerClassCode,
                            NationalCode = "",
                            Address = customer.DefultCustomerAddress.AddressText,
                            City = 1000000,
                            Province = 10,
                            Email = customer.EmailAddress,
                            Mobile = customer.MobileNumber,
                            PersonType = PersonTypeAccounting.Actual,
                            Tel = customer.DefultCustomerAddress.PhoneNumber,
                            CommercialCode = "",
                            PostalCode = "",
                            Spec1 = ""
                        };
                        string objStringify = JsonConvert.SerializeObject(createDetailRequest);
                        HttpContent content = new StringContent(objStringify, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = _httpClient.PostAsync(DetailServiceName, content).Result;
                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            var resultString = response.Content.ReadAsStringAsync().Result;
                            var result = long.Parse(JsonConvert.DeserializeObject<string>(resultString));
                            if (result > 0)
                            {
                                customer.RegisterInAccountingSystem(result);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            _logger.Error($"######accounting createDetailRequest customer web service  -- {DateTime.Now}");
                            _logger.Error(JsonConvert.SerializeObject(createDetailRequest));
                            _logger.Error(response);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error($"######exception accounting createDetailRequest customer web service  -- {DateTime.Now}");
                        _logger.Error(e);
                    }
                }
            }
        }
        public void RegisterShops()
        {
            using (var db = new ShoppingSchedulerContext())
            {
                var shops = db.Shops.Where(item => item.Accounting == null).ToList();
                foreach (var shop in shops)
                {
                    try
                    {
                        var createDetailRequest = new CreateDetailRequest
                        {
                            Code = 0,
                            RequestId = Guid.NewGuid(),
                            Name = $"{shop.FirstName} {shop.LastName}",
                            ClassCode = ShopClassCode,
                            NationalCode = shop.NationalCode,
                            Address = shop.ShopAddress.AddressText,
                            City = Convert.ToInt32(shop.ShopAddress.CityCode),
                            Province = Convert.ToInt32(shop.ShopAddress.ProvinceCode),
                            Email = shop.EmailAddress,
                            Mobile = shop.MobileNumber,
                            PersonType = PersonTypeAccounting.Actual,
                            Tel = shop.ShopAddress.PhoneNumber,
                            Spec1 = shop.BankAccount.Iban,
                            CommercialCode = "",
                            PostalCode = ""
                        };
                        string objStringify = JsonConvert.SerializeObject(createDetailRequest);
                        HttpContent content = new StringContent(objStringify, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = _httpClient.PostAsync(DetailServiceName, content).Result;
                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            var resultString = response.Content.ReadAsStringAsync().Result;
                            var result = long.Parse(JsonConvert.DeserializeObject<string>(resultString));
                            if (result > 0)
                            {
                                shop.RegisterInAccountingSystem(result);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            _logger.Error($"######accounting createDetailRequest shop web service  -- {DateTime.Now}");
                            _logger.Error(createDetailRequest);
                            _logger.Error(response);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error($"######exception accounting createDetailRequest shop web service  -- {DateTime.Now}");
                        _logger.Error(e);
                    }
                }
            }
        }
    }
}