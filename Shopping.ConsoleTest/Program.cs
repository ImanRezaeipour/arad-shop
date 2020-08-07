

// ReSharper disable ConditionIsAlwaysTrueOrFalse

using System;

namespace Shopping.ConsoleTest
{
    //http://www.michael-whelan.net/rules-design-pattern/
    class Program
    {
        static void Main()
        {
            if (DateTime.Now >= DateTime.Parse("2019/05/19"))
            {
                
            }
        }

        private static decimal RoundPrice(decimal price)
        {
            var toman = price / 10;
            var round = decimal.Floor(toman);
            var dahganYekan = round % 100;
            if (dahganYekan < 50)
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
