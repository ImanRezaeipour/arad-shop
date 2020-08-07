using System;
using System.Linq;
using NLog;
using Shopping.Infrastructure.Enum;
using Shopping.Scheduler.Core.Contexts;
using Shopping.Scheduler.Core.Interfaces;

namespace Shopping.Scheduler.Core.Services
{
    public class CalculateProductPriceService : ICalculateProductPriceService
    {
        private readonly Logger _logger;
        public CalculateProductPriceService()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
        public void Calculate()
        {
            using (var db = new ShoppingSchedulerContext())
            {
                var yesterday = DateTime.Today.AddDays(-1);
                var yesterdayNight = DateTime.Today.AddDays(-1) + TimeSpan.Parse("23:59:59");

                var yesterdayPaidFactorsRows = db.Factors.Where(p =>
                    p.FactorState == FactorState.Paid && p.CreationTime >= yesterday &&
                    p.CreationTime <= yesterdayNight).SelectMany(p => p.FactorRaws);

                var productCount = db.Products.Count();
                var productsSort = db.Products.OrderByDescending(item => item.Id);
                for (var i = 0; i < productCount; i = i + 100)
                {
                    var products = productsSort.Skip(i).Take(100).ToList();
                    foreach (var product in products)
                    {
                        var factorItems = yesterdayPaidFactorsRows.Where(item =>
                            item.ProductId == product.Id).ToList();
                        decimal sum = 0;
                        foreach (var factorItem in factorItems)
                        {
                            sum += factorItem.Price;
                        }
                        if (factorItems.Any())
                        {
                            var avg = sum / factorItems.Count;
                            var price = RoundPrice(avg);
                            _logger.Error($"{product.Id}--{product.Name}--{product.Price} changed to {price}");
                            product.Price = price;
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                _logger.Error(e, $"{product} savechange exception");
                            }
                        }
                    }
                }
            }
        }
        private static decimal RoundPrice(decimal price)
        {
            var toman = price / 10;
            var round = decimal.Floor(toman);
            var dahganYekan = round % 100;
            if (dahganYekan < 50 && dahganYekan > 0)
            {
                var baghi = 50 - dahganYekan;
                round += baghi;
            }
            else if (dahganYekan > 50)
            {
                var baghi = 100 - dahganYekan;
                round += baghi;
            }
            return round * 10;
        }
    }
}