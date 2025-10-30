using DNTPersianUtils.Core;
using System;
using System.Globalization;

namespace _0_Framework.Application
{
    public class DateHelper
    {
        public static string GetPersianDate()
        {
            return DateTime.Now.ToPersianDateTextify(); // چهارشنبه، 15 مرداد 1404
        }

        public static string GetGregorianDate()
        {
            return DateTime.Now.ToString("dddd, dd MMMM yyyy", new CultureInfo("en-US")); // Wednesday, 06 August 2025
        }
    }
}
