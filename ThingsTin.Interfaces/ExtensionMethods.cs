using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsTin.Interfaces
{
    public static class ExtensionMethods
    {
        public static int ToInt(this string value)
        {
            int dbe = 0;
            int.TryParse(value, out dbe);
            return dbe;
        }

        public static double ToDouble(this string value)
        {
            double dbe = 0;
            double.TryParse(value, out dbe);
            return dbe;
        }

        public static DateTime ToDate(this string value)
        {
            DateTime dt;
            bool bln =DateTime.TryParse(value, out dt);
            if (bln)
            {
                return dt.Date;
            }

            if (value.Length == 8)
            {
                return new DateTime(int.Parse(value.Substring(0, 4)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(6, 2)));
            }

            return DateTime.MinValue;
        }

        public static DateTime ToDateTime(this string value)
        {
            DateTime dt;
            bool bln = DateTime.TryParse(value, out dt);
            if (bln)
            {
                return dt;
            }

            if (value.Length == 8)
            {
                return new DateTime(2000, 1, 1, int.Parse(value.Substring(0, 4)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(6, 2)));
            }

            return DateTime.MinValue;
        }

        public static bool ToBool(this string value)
        {
            bool result;
            bool.TryParse(value, out result);
            return result;
        }

        public static int RoundUp(this double value)
        {
            if (value > ((int)value))
            {
                return ((int)value) + 1;
            }

            return (int)value;
        }

        public static string ToWeekDay(this DateTime day, out bool isWeekend)
        {
            var weekdays = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            DayOfWeek dow = day.DayOfWeek;
            isWeekend = dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday ? true : false;
            return weekdays[(int)dow];
        }
    }
}
