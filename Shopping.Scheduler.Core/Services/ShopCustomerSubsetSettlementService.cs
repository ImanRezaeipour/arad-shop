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
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.DomainModel.Aggregates.ShopCustomerSubsetSettlements.Aggregates;
using Shopping.Infrastructure.Enum;
using Shopping.Scheduler.Core.Contexts;
using Shopping.Scheduler.Core.Interfaces;
using Shopping.Scheduler.Core.Messages.Accounting.Factors.GRDB;

namespace Shopping.Scheduler.Core.Services
{
    public class ShopCustomerSubsetSettlementService : IShopCustomerSubsetSettlementService
    {
        private readonly Logger _logger;
        private const string GrdbServiceName = "GRDB";
        private readonly HttpClient _httpClient;
        public ShopCustomerSubsetSettlementService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["AccountingBaseUrl"]),
                Timeout = TimeSpan.FromSeconds(30)
            };
            _logger = LogManager.GetCurrentClassLogger();
        }
        public void SettlementPaidFactor()
        {
            using (var db = new ShoppingSchedulerContext())
            {
                var shops = db.Shops.Where(item => item.CustomerSubsets
                        .Any(p => !p.IsSettlement && p.HavePaidFactor))
                    .ToList();

                foreach (var shop in shops)
                {
                    var appSetting = db.Settings.FirstOrDefault();
                    if (appSetting == null)
                        return;

                    var sumShopCustomerSubsetHaveFactorPaidSumAmount =
                        shop.CustomerSubsets.Count(p => !p.IsSettlement && p.HavePaidFactor) *
                        appSetting.ShopCustomerSubsetHaveFactorPaidAmount;

                    var shopCustomerSubsetSettlement = new ShopCustomerSubsetSettlement(Guid.NewGuid(), shop, new UserInfo(Guid.NewGuid(), "سیستم", "سیستم"),
                         sumShopCustomerSubsetHaveFactorPaidSumAmount, ShopCustomerSubsetSettlementType.PaidFactor);
                    db.ShopCustomerSubsetSettlements.Add(shopCustomerSubsetSettlement);
                    foreach (var shopCustomerSubset in shop.CustomerSubsets.Where(p => !p.IsSettlement && p.HavePaidFactor))
                    {
                        shopCustomerSubset.SetSettlement();
                    }
                    db.SaveChanges();
                }
            }
        }

        public void RegisterSettlementPaidFactor()
        {
            using (var db = new ShoppingSchedulerContext())
            {
                var list = db.ShopCustomerSubsetSettlements.Where(p =>
                    !p.IsRegisteredInAccounting && p.Type == ShopCustomerSubsetSettlementType.PaidFactor).ToList();
                foreach (var shopCustomerSubsetSettlement in list)
                {
                    try
                    {
                        var temp = new Grdb
                        {
                            TypeNo = 200,
                            StrDate = shopCustomerSubsetSettlement.CreationTime.ToFa(),
                            DetailCode1 = shopCustomerSubsetSettlement.Shop.Accounting.DetailCode,
                            RequestId = Guid.NewGuid(),
                            Items = new List<GrdbItem>
                            {
                                new GrdbItem
                                {
                                    GoodCode = "85105",
                                    Amount = 1,
                                    Fee = shopCustomerSubsetSettlement.Amount,
                                    ItemDesc1 =$"ثبت هزینه کارمزد اولین خرید مشتری جذب شده فروشگاه {shopCustomerSubsetSettlement.Shop.NationalCode}"
                                }
                            }
                        };
                        string objStringify = JsonConvert.SerializeObject(temp);
                        HttpContent content = new StringContent(objStringify, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = _httpClient.PostAsync(GrdbServiceName, content).Result;
                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            var resultString = response.Content.ReadAsStringAsync().Result;
                            var result = JsonConvert.DeserializeObject<Grdb>(resultString);
                            if (result.Id >= 0)
                            {
                                shopCustomerSubsetSettlement.RegisterInAccounting(result.Id.ToString());
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            _logger.Error($"######accounting RegisterShopCustomerSubsetSettlementAccountingService GRDB web service  -- {DateTime.Now}");
                            _logger.Error(temp);
                            _logger.Error(response);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error($"######exception accounting RegisterShopCustomerSubsetSettlementAccountingService GRDB web service   -- {DateTime.Now}");
                        _logger.Error(e);
                    }
                }
            }
        }
    }
}