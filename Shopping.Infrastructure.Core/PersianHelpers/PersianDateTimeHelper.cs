using System;
using System.Globalization;

namespace Shopping.Infrastructure.Core.PersianHelpers
{
    public static class PersianDateTimeHelper
    {
        public static readonly PersianCalendar PersianCalendar = new PersianCalendar();


        /// <summary>
        /// تبدیل رشته تاریخ شمسی به تاریخ میلادی با ساعت صفر
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static DateTime ConvertToDate(this string src)
        {
            try
            {
                var persianDatespl = src.Split('/');
                var year = int.Parse(persianDatespl[0]);
                var month = int.Parse(persianDatespl[1]);
                var day = int.Parse(persianDatespl[2]);


                return PersianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (Exception)
            {
                throw new FormatException();
            }
        }

        /// <summary>
        /// تبدیل رشته تاریخ وساعت شمسی به تاریخ و ساعت میلادی با ساعت صفر
        /// </summary>
        /// <param name="src"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this string src, string time)
        {
            try
            {
                var persianDatespl = src.Split('/');
                var year = int.Parse(persianDatespl[0]);
                var month = int.Parse(persianDatespl[1]);
                var day = int.Parse(persianDatespl[2]);


                return DateTime.Parse(string.Format("{0} {1}", PersianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0).ToString("yyyy MM dd"), time));
            }
            catch (Exception)
            {
                throw new FormatException();
            }
        }



        /// <summary>
        /// روز شمسی معادل تاریخ میلادی
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static int FaDayOfMonth(this DateTime src)
        {
            return PersianCalendar.GetDayOfMonth(src);
        }

        /// <summary>
        /// ماه شمسی معادل تاریخ میلادی
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static int FaMonthOfYear(this DateTime src)
        {
            return PersianCalendar.GetMonth(src);
        }

        /// <summary>
        /// سال شمسی معادل تاریخ میلادی
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static int FaYear(this DateTime src)
        {
            return PersianCalendar.GetYear(src);
        }


        /// <summary>
        ///   اضافه کردن ماه شمسی به تاریخ معادل میلادی
        /// </summary>
        /// <param name="src"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FaAddMonths(this DateTime src, int value)
        {
            return PersianCalendar.AddMonths(src, value);
        }

        /// <summary>
        ///   اضافه کردن روز به تاریخ معادل میلادی
        /// </summary>
        /// <param name="src"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static DateTime FaAddDays(this DateTime src, int days)
        {
            return PersianCalendar.AddDays(src, days);
        }


        /// <summary>
        /// تاریخ شمسی معادل تاریخ میلادی
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string FaDate(this DateTime src)
        {
            return
                $"{PersianCalendar.GetYear(src)}/{PersianCalendar.GetMonth(src):d2}/{PersianCalendar.GetDayOfMonth(src):d2}";
        }


        /// <summary>
        /// تبدیل به تاریخ میلادی
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static DateTime ToDate(int year, int month, int day)
        {
            return PersianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0).Date;
        }

        /// <summary>
        /// تاریخ میلادی معادل اولین روز ماه شمسی
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static DateTime FaStartOfMonth(this DateTime src)
        {
            var year = PersianCalendar.GetYear(src);
            var month = PersianCalendar.GetMonth(src);
            var result = PersianCalendar.ToDateTime(year, month, 1, 0, 0, 0, 0);
            return result.Date;
        }

        /// <summary>
        /// تاریخ میلادی معادل اولین روز سال شمسی
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static DateTime FaStartOfYear(this DateTime src)
        {
            var year = PersianCalendar.GetYear(src);
            return FaStartOfYear(year);
        }

        /// <summary>
        /// تاریخ میلادی معادل ابتدای سال شمسی
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime FaStartOfYear(int year)
        {
            return PersianCalendar.ToDateTime(year, 1, 1, 0, 0, 0, 0).Date;
        }

        /// <summary>
        /// تاریخ میلادی معادل آخرین روز سال شمسی
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static DateTime FaEndOfYear(this DateTime src)
        {
            var year = PersianCalendar.GetYear(src);
            return FaEndOfYear(year);
        }
        /// <summary>
        /// تاریخ میلادی معادل آخرین روز سال شمسی
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime FaEndOfYear(int year)
        {
            var daysInMonth = PersianCalendar.GetDaysInMonth(year, 12);
            var result = PersianCalendar.ToDateTime(year, 12, daysInMonth, 0, 0, 0, 0);
            return result.Date;
        }

        /// <summary>
        /// ابتدای دوره براساس تاریخ و طول دوره را برمیگرداند
        /// </summary>
        /// <param name="src"></param>
        /// <param name="countOfMonth">طول دوره براساس ماه</param>
        /// <returns></returns>
        public static DateTime FaStartOfSeason(this DateTime src, int countOfMonth)
        {
            int monthOfYear = src.FaMonthOfYear();
            var begingOfPart = (((monthOfYear - 1) / countOfMonth) * countOfMonth) + 1;
            var runDate = PersianCalendar.ToDateTime(src.FaYear(), begingOfPart, 1, 0, 0, 0, 0);
            return runDate;
        }



        /// <summary>
        /// تاریخ میلادی معادل اخرین روز ماه شمسی براساس ماه مورد نظر
        /// </summary>
        /// <param name="src"></param>
        /// <param name="month">ماه مورد نظر</param>
        /// <returns></returns>
        public static DateTime FaStartOfMonth(this DateTime src, int month)
        {
            var year = PersianCalendar.GetYear(src);
            var result = PersianCalendar.ToDateTime(year, month, 1, 0, 0, 0, 0);
            return result.Date;
        }

        /// <summary>
        /// تاریخ میلادی معادل آخرین روز ماه شمسی
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static DateTime FaEndOfMonth(this DateTime src)
        {
            var year = PersianCalendar.GetYear(src);
            var month = PersianCalendar.GetMonth(src);
            var daysInMonth = PersianCalendar.GetDaysInMonth(year, month);

            var result = PersianCalendar.ToDateTime(year, month, daysInMonth, 0, 0, 0, 0);
            return result.Date;
        }

        /// <summary>
        /// تاریخ میلادی معادل اخرین روز ماه شمسی براساس ماه مورد نظر
        /// </summary>
        /// <param name="src"></param>
        /// <param name="month">ماه مورد نظر</param>
        /// <returns></returns>
        public static DateTime FaEndOfMonth(this DateTime src, int month)
        {
            var year = PersianCalendar.GetYear(src);
            var daysInMonth = PersianCalendar.GetDaysInMonth(year, month);

            var result = PersianCalendar.ToDateTime(year, month, daysInMonth, 0, 0, 0, 0);
            return result.Date;
        }

        /// <summary>
        /// نام روز هفته براساس تاریخ میلادی
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string FaWeekDayName(this DateTime src)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(src.DayOfWeek);
        }


        /// <summary>
        /// صفر کردن ثانیه و میلی ثانیه زمان
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static DateTime FloorByMinuts(this DateTime src)
        {
            return new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0, 0);
        }

        public static DateTime ConvertGregorianDateTime(this string date)
        {
            var split = date.Split('/');
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(Int32.Parse(split[0]), Int32.Parse(split[1]), Int32.Parse(split[2]), pc);
            return dt;
        }
    }
}