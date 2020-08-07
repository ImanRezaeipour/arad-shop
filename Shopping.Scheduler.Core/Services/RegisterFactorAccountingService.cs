using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using NLog;
using PersianDate;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Entities.State;
using Shopping.Infrastructure.Enum;
using Shopping.Scheduler.Core.Contexts;
using Shopping.Scheduler.Core.Interfaces;
using Shopping.Scheduler.Core.Messages.Accounting.Factors.GRDB;
using Shopping.Scheduler.Core.Messages.Accounting.Factors.RecPay;

namespace Shopping.Scheduler.Core.Services
{
    public class RegisterFactorAccountingService : IRegisterFactorAccountingService
    {
        private readonly Logger _logger;
        private readonly HttpClient _httpClient;
        private const string FactorServiceName = "GRDB";
        private const string RecPayServiceName = "RecPay ";
        private const int DetailCode3 = 100002;
        private const string GoodCode = "301001";
        private const int Id = 2323;
        private const short TypeNo = 100;
        private const short AddorRed = -1;
        private const short RowId = 1;
        private const short Per = 0;
        public RegisterFactorAccountingService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["AccountingBaseUrl"]),
                Timeout = TimeSpan.FromSeconds(30)
            };
            _logger = LogManager.GetCurrentClassLogger();
        }
        public void RegisterFactors()
        {
            using (var db = new ShoppingSchedulerContext())
            {
                var factors = db.Factors.Where(item => item.FactorState == FactorState.Paid
                                                       && item.Accounting == null).OrderByDescending(p => p.CreationTime).ToList();
                foreach (var factor in factors)
                {
                    if (CallGrdbService(factor))
                    {
                        CallRecPayService(factor);
                    }
                    db.SaveChanges();
                }
            }
        }

        private void CallRecPayService(Factor factor)
        {
            try
            {
                var factorState = factor.FactorStateBase as PaidFactorState;
                var factorRequest = new FactorRecPay
                {
                    Desc = $"خرید {factor.Customer.FirstName} {factor.Customer.LastName}",
                    DetailCode2 = DetailCode3,
                    RequestId = Guid.NewGuid(),
                    RPCode = 1,
                    DevId = 100,
                    strDate = factor.CreationTime.ToFa(),
                    CheqNo = factorState.ReferenceId,
                    RecPayItems = new List<RecPayItem>
                    {
                        new RecPayItem
                        {
                            RPID = 101001,
                            DetailCode1 = factor.Customer.Accounting.DetailCode,
                            RowDesc = $"فروش {factor.FactorRaws.Sum(p=>p.Quantity)} قلم کالا",
                            Price = factor.SystemDiscountPrice
                        }
                    }
                };
                string objStringify = JsonConvert.SerializeObject(factorRequest);
                HttpContent content = new StringContent(objStringify, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(RecPayServiceName, content).Result;
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<FactorRecPay>(resultString);
                    factor.RegisterRecPayInAccountingSystem();
                }
                else
                {
                    _logger.Error($"######accounting RegisterInAccountingSystem RecPay web service  -- {DateTime.Now}");
                    _logger.Error(JsonConvert.SerializeObject(factorRequest));
                    _logger.Error(response);
                }
            }
            catch (Exception e)
            {
                _logger.Error($"######exception accounting RegisterInAccountingSystem RecPay web service  -- {DateTime.Now}");
                _logger.Error(e);
            }
        }
        private bool CallGrdbService(Factor factor)
        {
            try
            {
                var factorRequest = new Grdb
                {
                    // ReSharper disable once PossibleNullReferenceException
                    Desc1 = factor.Id.ToString(),
                    DetailCode1 = factor.Shop.Accounting.DetailCode,
                    DetailCode2 = factor.Customer.Accounting.DetailCode,
                    RequestId = Guid.NewGuid(),
                    DetailCode3 = DetailCode3,
                    StrDate = factor.CreationTime.ToFa(),
                    TypeNo = TypeNo,
                    Items = new List<GrdbItem>
                    {
                        new GrdbItem
                        {
                            GoodCode = GoodCode,
                            Amount = 1,
                            Fee = factor.DiscountPrice,
                            ItemDesc1 = $"فروش {factor.FactorRaws.Sum(p=>p.Quantity)} قلم کالا"
                        }
                    },
                    AddReds = new List<AddOrRed>
                    {
                        new AddOrRed
                        {
                            Id = Id,
                            AddorRed = AddorRed,
                            RowId = RowId,
                            Per = Per,
                            Price = factor.DiscountPrice - factor.SystemDiscountPrice
                        }
                    }
                };
                string objStringify = JsonConvert.SerializeObject(factorRequest);
                HttpContent content = new StringContent(objStringify, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(FactorServiceName, content).Result;
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<Grdb>(resultString);
                    if (result.Id >= 0)
                    {
                        factor.RegisterInAccountingSystem(result.Id.ToString());
                        return true;
                    }
                    return false;
                }
                else
                {
                    _logger.Error($"######accounting RegisterInAccountingSystem GRDB web service  -- {DateTime.Now}");
                    _logger.Error(JsonConvert.SerializeObject(factorRequest));
                    _logger.Error(response);
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.Error($"######exception accounting RegisterInAccountingSystem GRDB web service   -- {DateTime.Now}");
                _logger.Error(e);
                return false;
            }
        }
    }
}