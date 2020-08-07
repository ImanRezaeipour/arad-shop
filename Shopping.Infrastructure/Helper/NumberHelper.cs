using System;

namespace Shopping.Infrastructure.Helper
{
    public static class LongExtension
    {
        public static string ToCommaSplittedString(this long number)
        {
            return number.ToString("#,##0");
        }

    }

    public static class DecimalExtension
    {
        public static string ToCommaSplittedString(this decimal number)
        {
            return number.ToString("#,##0");
        }

    }

    public static class NumberHelper
    {
        private static readonly string[] Yakan = { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static readonly string[] Dahgan = { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static readonly string[] Dahyek = { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static readonly string[] Sadgan = { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static readonly string[] Basex = { "", "هزار", "میلیون", "میلیارد", "تریلیون" };
        private static string Getnum3(int num2)
        {
            int num3 = (num2 < 0 ? num2 * -1 : num2);


            string s = "";
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = Sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + Dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + Dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + Yakan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            }
            return s;
        }
        public static string Num2Str(string snum)
        {
            string stotal = "";
            if (snum == "") return "صفر";
            if (snum == "0")
            {
                return Yakan[0];
            }
            else
            {
                snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
                int l = snum.Length / 3 - 1;
                for (int i = 0; i <= l; i++)
                {
                    int b = int.Parse(snum.Substring(i * 3, 3));
                    if (b != 0)
                        stotal = stotal + Getnum3(b) + " " + Basex[l - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            return stotal;
        }
    }
    public static class DoubleHelper
    {
        private static readonly double ErrorRate = 0.00000002;
        public static bool EqualAccurate(this double x, double y)
        {
            return Math.Abs(x - y) < ErrorRate;
        }
    }
}