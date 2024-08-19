using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace Phoenix.LoadTests
{
    [Description("Generates a Random Start Date ; Start Time; End Date and End Time. End Date and time will happen five minutes after the start date and time")]
    public class GenerateDatesPlugin : WebTestPlugin
    {
        [Description("The name of the context parameter that will hold the value for the start date")]
        public string StartDateContextParameterName { get; set; }

        [Description("The name of the context parameter that will hold the value for the end date")]
        public string StartTimeContextParameterName { get; set; }

        [Description("The name of the context parameter that will hold the value for the start time")]
        public string EndDateContextParameterName { get; set; }

        [Description("The name of the context parameter that will hold the value for the end time")]
        public string EndTimeContextParameterName { get; set; }


        [Description("the format to output the start date and end date. E.g. 'dd/MM/yyyu' or 'yyyy-MM-dd' , etc... ")]
        public string DateOutputFormat { get; set; }




        [Description("The months limit for the date to be generated (date generated will be between the current date and the current date plus number of months)")]
        public int MaximumMonthsInTheFuture { get; set; }

        [Description("The the minimum allowed hour")]
        public int MinimumHour { get; set; }

        [Description("The the maximum allowed hour")]
        public int MaximumHour { get; set; }

        public override void PreWebTest(object sender, PreWebTestEventArgs e)
        {
            Random r = new Random();

            DateTime StartDate = GetRandomDate(r, DateTime.Now.Date, DateTime.Now.Date.AddMonths(MaximumMonthsInTheFuture));
            TimeSpan StartTime = GetRandomTime(r, TimeSpan.FromHours(MinimumHour), TimeSpan.FromHours(MaximumHour));
            DateTime EndDate = StartDate;
            TimeSpan EndTime = StartTime.Add(TimeSpan.FromMinutes(5));
            if(string.IsNullOrEmpty(DateOutputFormat))
                e.WebTest.Context.Add(this.StartDateContextParameterName, StartDate.ToString("dd/MM/yyyy"));
            else
                e.WebTest.Context.Add(this.StartDateContextParameterName, StartDate.ToString(DateOutputFormat));

            e.WebTest.Context.Add(this.StartTimeContextParameterName, StartTime.ToString(@"hh\:mm"));

            if (string.IsNullOrEmpty(DateOutputFormat))
                e.WebTest.Context.Add(this.EndDateContextParameterName, EndDate.ToString("dd/MM/yyyy"));
            else
                e.WebTest.Context.Add(this.EndDateContextParameterName, EndDate.ToString(DateOutputFormat));

            e.WebTest.Context.Add(this.EndTimeContextParameterName, EndTime.ToString(@"hh\:mm"));
        }


        private DateTime GetRandomDate(Random gen, DateTime MinDate, DateTime MaxDate)
        {
            int range = (MaxDate - MinDate).Days;
            return MinDate.AddDays(gen.Next(range));
        }

        private TimeSpan GetRandomTime(Random random, TimeSpan start, TimeSpan end)
        {
            int maxMinutes = (int)((end - start).TotalMinutes);
            int minutes = (random.Next(maxMinutes) / 5) * 5;
            return start.Add(TimeSpan.FromMinutes(minutes));

        }

    }
}
