using System;
using System.Globalization;

namespace MyCms.Classes
{
    public static class PersianConvertorDate
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00")  
                                + "/" + pc.GetDayOfMonth(value).ToString("00");
        }
    }
}
