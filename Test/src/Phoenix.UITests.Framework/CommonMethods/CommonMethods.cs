using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.CommonMethods
{
    public class CommonMethods
    {
        private Random gen = new Random();
        internal DateTime aDate = DateTime.Now;
        internal DateTime aDate_UTC;
        internal string aDateAfterCurrentDate = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
        internal string aDateBeforeCurrentDate = DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy");
        internal string aTime_UTC = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local).ToString("hh:mm");

        public DateTime GenerateRandomDate(DateTime MinDate, DateTime MaxDate)
        {
            int range = (MaxDate - MinDate).Days;
            return MinDate.AddDays(gen.Next(range));
        }

        public string GetCurrentDate()
        {
            aDate_UTC = DateTime.UtcNow;
            return aDate_UTC.ToString("dd'/'MM'/'yyyy");
        }

        public string GetCurrentDateTimeString(bool WithMilliseconds = false)
        {
            aDate_UTC = DateTime.UtcNow;
            if (WithMilliseconds)
                return aDate_UTC.ToString("yyyyMMddHHmmssf");

            return aDate_UTC.ToString("yyyyMMddHHmmss");
        }

        public DateTime GetDatePartWithoutCulture()
        {
            aDate = DateTime.Now;
            return new DateTime(aDate.Year, aDate.Month, aDate.Day);
        }

        public DateTime GetCurrentDateWithoutCulture(bool WithMilliseconds = false)
        {
            aDate = DateTime.Now;

            if (WithMilliseconds)
                return new DateTime(aDate.Year, aDate.Month, aDate.Day, aDate.Hour, aDate.Minute, aDate.Second, aDate.Millisecond);

            return new DateTime(aDate.Year, aDate.Month, aDate.Day, aDate.Hour, aDate.Minute, aDate.Second);
        }

        public DateTime GetDateWithoutCulture(DateTime DateToRemoveCulture)
        {
            return new DateTime(DateToRemoveCulture.Year, DateToRemoveCulture.Month, DateToRemoveCulture.Day, DateToRemoveCulture.Hour, DateToRemoveCulture.Minute, DateToRemoveCulture.Second);
        }

        public string GetOlderDate()
        {
            aDateBeforeCurrentDate = DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy");
            return aDateBeforeCurrentDate;
        }

        public string GetFutureDate()
        {
            aDateAfterCurrentDate = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
            return aDateAfterCurrentDate;
        }

        public int GetRandomValue(int MinValue, int MaxValue)
        {
            return gen.Next(MinValue, MaxValue);
        }

        public DateTime GetThisWeekFirstMonday()
        {
            DateTime dt = DateTime.Now;
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            var tempDate = dt.AddDays(-1 * diff).Date;

            return new DateTime(tempDate.Year, tempDate.Month, tempDate.Day);
        }

        public DateTime GetWeekFirstMonday(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            var tempDate = date.AddDays(-1 * diff).Date;

            return new DateTime(tempDate.Year, tempDate.Month, tempDate.Day);
        }

        public DateTime GetFirstMondayOfMonthAndYear(int Month, int Year)
        {
            DateTime dt = new DateTime(Year, Month, 1);

            for (int i = 0; i < 7; i++)
            {
                if (dt.DayOfWeek == DayOfWeek.Monday)
                {
                    return dt;
                }
                else
                {
                    dt = dt.AddDays(1);
                }
            }

            // If get to here, punt
            return DateTime.Now;
        }

        public DateTime GetDayOfWeek(DateTime date, DayOfWeek dayOfTheWeek)
        {
            var dt = date.Date.AddDays(1);
            var days = ((int)dayOfTheWeek - (int)dt.DayOfWeek) % 7;
            return dt.AddDays(days);
        }

        public DateTime GetLastDayOfMonth(DateTime date)
        {
            int day = DateTime.DaysInMonth(date.Year, date.Month);
            return new DateTime(date.Year, date.Month, day);
        }

        public DateTime GetLastWeekdayOfMonth(DateTime date, DayOfWeek day)
        {
            DateTime lastDayOfMonth = new DateTime(date.Year, date.Month, 1)
                .AddMonths(1).AddDays(-1);
            int wantedDay = (int)day;
            int lastDay = (int)lastDayOfMonth.DayOfWeek;

            return lastDayOfMonth.AddDays(
                lastDay >= wantedDay ? wantedDay - lastDay : wantedDay - lastDay - 7);
        }

        public int GetWallchartDayOfWeekIntegerValue(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return 7;
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                default:
                    return 0;
            }
        }

    }
}
