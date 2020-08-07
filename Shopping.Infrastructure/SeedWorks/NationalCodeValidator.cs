using System;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using Shopping.Infrastructure.Core;

namespace Shopping.Infrastructure.SeedWorks
{
    public static class NationalCodeValidator
    {
        /// <summary>
        /// تعیین معتبر بودن کد ملی
        /// </summary>
        /// <param name="nationalCode">کد ملی وارد شده</param>
        /// <returns>
        /// در صورتی که کد ملی صحیح باشد خروجی <c>true</c> و در صورتی که کد ملی اشتباه باشد خروجی <c>false</c> خواهد بود
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public static Boolean IsValidNationalCode(this string nationalCode)
        {
            //در صورتی که کد ملی وارد شده تهی باشد

            if (string.IsNullOrEmpty(nationalCode))
                throw new DomainException("لطفا کد ملی را صحیح وارد نمایید");

            //-> اتباع بیگانه
            if (nationalCode.Substring(0, 1) == "9")
            {
                return true;
            }

            //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
            if (nationalCode.Length != 10)
                throw new DomainException("طول کد ملی باید ده کاراکتر باشد");

            //در صورتی که کد ملی ده رقم عددی نباشد
            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
                throw new DomainException("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            var allDigitEqual = new[]
            {
                "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666",
                "7777777777", "8888888888", "9999999999"
            };
            if (((IList)allDigitEqual).Contains(nationalCode)) return false;


            //عملیات اعتبار سنجی با الگوریتم
            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString(CultureInfo.InvariantCulture)) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString(CultureInfo.InvariantCulture)) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString(CultureInfo.InvariantCulture)) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString(CultureInfo.InvariantCulture)) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString(CultureInfo.InvariantCulture)) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString(CultureInfo.InvariantCulture)) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString(CultureInfo.InvariantCulture)) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString(CultureInfo.InvariantCulture)) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString(CultureInfo.InvariantCulture)) * 2;
            var a = Convert.ToInt32(chArray[9].ToString(CultureInfo.InvariantCulture));

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }
    }
}
