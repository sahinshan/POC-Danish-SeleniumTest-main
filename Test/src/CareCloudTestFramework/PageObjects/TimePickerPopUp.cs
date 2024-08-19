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
    public class TimePickerPopUp : CommonMethods
    {
        readonly Func<AppQuery, AppQuery> _parentPanel = e => e.Id("parentPanel");

        readonly Func<AppQuery, AppQuery> _timePicker = e => e.Id("timePicker");


        readonly Func<AppQuery, AppQuery> _setButton = e => e.Id("button1");
        readonly Func<AppQuery, AppQuery> _cancelButton = e => e.Id("button2");
        readonly Func<AppQuery, AppQuery> _clearButton = e => e.Id("button3");

        readonly Func<AppQuery, AppQuery> _inputTimeModeButton = e => e.Marked("toggle_mode");
        readonly Func<AppQuery, AppQuery> _hourText = e => e.Marked("input_hour");
        readonly Func<AppQuery, AppQuery> _minuteText = e => e.Marked("input_minute");



        public TimePickerPopUp(IApp app)
        {
            _app = app;

        }


        public TimePickerPopUp WaitForTimePickerPopUpToLoad()
        {
            _app.WaitForElement(_parentPanel);

            _app.WaitForElement(_timePicker);


            _app.WaitForElement(_setButton);
            _app.WaitForElement(_cancelButton);
            _app.WaitForElement(_clearButton);

            return this;
        }


        public TimePickerPopUp TapTimeInputMode()
        {
            _app.Tap(_inputTimeModeButton);

            return this;
        }

        public TimePickerPopUp setInputHourTime(string hour)
        {
            _app.ClearText(_hourText);
            _app.DismissKeyboard();
            _app.EnterText(_hourText, hour);
            _app.DismissKeyboard();

            return this;
        }

        public TimePickerPopUp setInputMinuteTime(string minute)
        {
            _app.ClearText(_minuteText);
            _app.DismissKeyboard();
            _app.EnterText(_minuteText, minute);
            _app.DismissKeyboard();

            return this;
        }

        public TimePickerPopUp TapTimeSetButton()
        {
            _app.Tap(_setButton);

            return this;
        }

    }     
}
