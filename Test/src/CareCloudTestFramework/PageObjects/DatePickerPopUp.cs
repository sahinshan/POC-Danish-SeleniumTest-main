using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest.Configuration;
using Xamarin.UITest;
using Xamarin.UITest.Queries;


namespace CareCloudTestFramework.PageObjects
{
    public class DatePickerPopUp : CommonMethods
    {
        readonly Func<AppQuery, AppQuery> _parentPanel = e => e.Id("parentPanel");

        readonly Func<AppQuery, AppQuery> _datePicker = e => e.Id("datePicker");


        readonly Func<AppQuery, AppQuery> _setButton = e => e.Id("button1");
        readonly Func<AppQuery, AppQuery> _cancelButton = e => e.Id("button2");
        readonly Func<AppQuery, AppQuery> _clearButton = e => e.Id("button3");

        readonly Func<AppQuery, AppQuery> _inputTimeModeButton = e => e.Marked("toggle_mode");
        readonly Func<AppQuery, AppQuery> _hourText = e => e.Marked("input_hour");
        readonly Func<AppQuery, AppQuery> _minuteText = e => e.Marked("input_minute");



        public DatePickerPopUp(IApp app)
        {
            _app = app;

        }


        public DatePickerPopUp WaitForTimePickerPopUpToLoad()
        {
            _app.WaitForElement(_parentPanel);

            _app.WaitForElement(_datePicker);


            _app.WaitForElement(_setButton);
            _app.WaitForElement(_cancelButton);
            _app.WaitForElement(_clearButton);

            return this;
        }


        public DatePickerPopUp setDate(DateTime date)
        {
            _app.Query(x => x.Id("datePicker").Invoke("updateDate", date.Year, date.Month-1, date.Day));
            _app.Tap(_setButton);

            return this;
        }

      

       

    }     
}
