using System;
using System.Globalization;
using System.Text;

namespace _0_Framework.Application
{
    public static class Tools
    {
        public static string[] MonthNames =
            {"فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"};

        public static string[] DayNames = {"شنبه", "یکشنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه"};
        public static string[] DayNamesG = {"یکشنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه"};


        public static string ToFarsi(this DateTime? date)
        {
            try
            {
                if (date != null) return date.Value.ToFarsi();
            }
            catch (Exception)
            {
                return "";
            }

            return "";
        }

        public static string ToFarsi(this DateTime date)
        {
            if (date == new DateTime()) return "";
            var pc = new PersianCalendar();
            //return $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
            var dateString = $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";

            return dateString.ToFarsiNumbers();
        }
        public static string ToFarsiNumbers(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var englishNumbers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var persianNumbers = new[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };

            var output = new StringBuilder(input.Length);
            foreach (var ch in input)
            {
                int index = Array.IndexOf(englishNumbers, ch);
                output.Append(index >= 0 ? persianNumbers[index] : ch);
            }
            return output.ToString();
        }


        public static DateTime? PersianToGregorian(string persianDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(persianDate))
                    return null;

                var parts = persianDate.Split('/');
                if (parts.Length != 3) return null;

                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int day = int.Parse(parts[2]);

                var pc = new PersianCalendar();
                return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch
            {
                return null;
            }
        }

        public static string ToDiscountFormat(this DateTime date)
        {
            if (date == new DateTime()) return "";
            return $"{date.Year}/{date.Month}/{date.Day}";
        }

        public static string GetTime(this DateTime date)
        {
            return $"_{date.Hour:00}_{date.Minute:00}_{date.Second:00}";
        }

        public static string ToFarsiFull(this DateTime date)
        {
            var pc = new PersianCalendar();
            return
                $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00} {date.Hour:00}:{date.Minute:00}:{date.Second:00}";
        }

        private static readonly string[] Pn = {"۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹"};
        private static readonly string[] En = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

        public static string ToEnglishNumber(this string strNum)
        {
            var cash = strNum;
            for (var i = 0; i < 10; i++)
                cash = cash.Replace(Pn[i], En[i]);
            return cash;
        }

        //public static string ToPersianNumber(this int intNum)
        //{
        //    var chash = intNum.ToString();
        //    for (var i = 0; i < 10; i++)
        //        chash = chash.Replace(En[i], Pn[i]);
        //    return chash;
        //}


            public static string ToPersianNumbers(string input)
            {
                if (string.IsNullOrWhiteSpace(input)) return input;

                var englishDigits = "0123456789";
                var persianDigits = "۰۱۲۳۴۵۶۷۸۹";

                foreach (var digit in englishDigits)
                    input = input.Replace(digit, persianDigits[englishDigits.IndexOf(digit)]);

                // افزودن کاراکتر مخفی راست‌به‌چپ برای جلوگیری از به‌هم‌ریختگی
                return "\u200F" + input;
            }






        public static DateTime? FromFarsiDate(this string InDate)
        {
            if (string.IsNullOrEmpty(InDate))
                return null;

            var spited = InDate.Split('/');
            if (spited.Length < 3)
                return null;

            if (!int.TryParse(spited[0].ToEnglishNumber(), out var year))
                return null;

            if (!int.TryParse(spited[1].ToEnglishNumber(), out var month))
                return null;

            if (!int.TryParse(spited[2].ToEnglishNumber(), out var day))
                return null;
            var c = new PersianCalendar();
            return c.ToDateTime(year, month, day, 0, 0, 0, 0);
        }


        public static DateTime ToGeorgianDateTime(this string persianDate)
        {
            persianDate = persianDate.ToEnglishNumber();
            var year = Convert.ToInt32(persianDate.Substring(0, 4));
            var month = Convert.ToInt32(persianDate.Substring(5, 2));
            var day = Convert.ToInt32(persianDate.Substring(8, 2));
            return new DateTime(year, month, day, new PersianCalendar());
        }

        public static string ToMoney(this double myMoney)
        {
            return myMoney.ToString("N0", CultureInfo.CreateSpecificCulture("fa-ir"));
        }

        public static string ToFileName(this DateTime date)
        {
            return $"{date.Year:0000}-{date.Month:00}-{date.Day:00}-{date.Hour:00}-{date.Minute:00}-{date.Second:00}";
        }

        public static (DateTime Start, DateTime End) GetPersianMonthRange(int year, int month)
        {
            var pc = new PersianCalendar();
            var start = pc.ToDateTime(year, month, 1, 0, 0, 0, 0);
            var daysInMonth = month == 12 ?
                (pc.IsLeapYear(year)) ? 30 : 29 :
                pc.GetDaysInMonth(year, month);
            var end = pc.ToDateTime(year, month, daysInMonth, 23, 59, 59, 999);
            return (start, end);
        }
    }
}