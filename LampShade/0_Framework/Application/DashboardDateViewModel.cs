using System;
using System.Globalization;

public class DashboardDateViewModel
{
    public string PersianDate { get; set; }
    public string GregorianDate { get; set; }

    public DashboardDateViewModel()
    {
        var now = DateTime.Now;

        // میلادی به فرمت کامل
        GregorianDate = now.ToString("dddd, dd MMMM yyyy", new CultureInfo("en-US"));

        // شمسی به فرمت کامل
        var persianCalendar = new PersianCalendar();
        var dayOfWeek = GetPersianDayOfWeek(now.DayOfWeek);
        var day = persianCalendar.GetDayOfMonth(now);
        var month = GetPersianMonthName(persianCalendar.GetMonth(now));
        var year = persianCalendar.GetYear(now);

        PersianDate = $"{dayOfWeek}، {day} {month} {year}";
    }

    private string GetPersianDayOfWeek(DayOfWeek day)
    {
        return day switch
        {
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یک‌شنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه‌شنبه",
            DayOfWeek.Wednesday => "چه‌شنبه",
            DayOfWeek.Thursday => "پنج‌شنبه",
            DayOfWeek.Friday => "جمعه",
            _ => ""
        };
    }

    private string GetPersianMonthName(int month)
    {
        return month switch
        {
            1 => "فروردین",
            2 => "اردیبهشت",
            3 => "خرداد",
            4 => "تیر",
            5 => "مرداد",
            6 => "شهریور",
            7 => "مهر",
            8 => "آبان",
            9 => "آذر",
            10 => "دی",
            11 => "بهمن",
            12 => "اسفند",
            _ => ""
        };
    }
}
