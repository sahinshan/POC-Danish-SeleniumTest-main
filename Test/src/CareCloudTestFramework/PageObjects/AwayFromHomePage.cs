using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

namespace CareCloudTestFramework.PageObjects
{
    public class AwayFromHomePage : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppWebQuery> _cancelButton = e => e.XPath("//button/span[text()='Cancel']");
        readonly Func<AppQuery, AppWebQuery> _saveNCloseButton = e => e.XPath("//button/span[text()='Save and close']");
        readonly Func<AppQuery, AppWebQuery> _awayFromHomeHeader = e => e.XPath("//div[text()='Away From Home']");
        Func<AppQuery, AppWebQuery> _AbsenceReason(string dataid) => e => e.XPath("//input[@value='" + dataid + "']/parent::label");
        Func<AppQuery, AppWebQuery> _PlannedStartDate => e => e.XPath("//input[@id='away-from-home-planned-start-date']");
        Func<AppQuery, AppWebQuery> _PlannedStartTime => e => e.XPath("//input[@id='away-from-home-planned-start-time']");

        Func<AppQuery, AppWebQuery> _PlannedEndDate => e => e.XPath("//input[@id='away-from-home-planned-end-date']");
        Func<AppQuery, AppWebQuery> _PlannedEndTime => e => e.XPath("//input[@id='away-from-home-planned-end-time']");
        Func<AppQuery, AppWebQuery> _CareNotesTextAea => e => e.XPath("//*[@id='careNotes']");
        Func<AppQuery, AppWebQuery> _PersonInfoHeader(string personfullname, int number) => e => e.XPath("//span[text()=' " + personfullname + " (ID " + number + ")']");



        public AwayFromHomePage(IApp app)
        {
            _app = app;

        }

        public AwayFromHomePage waitForAwayFromHomePageToLoad(string fullname, int number)
        {
            WaitForElement(_PersonInfoHeader(fullname, number));
            WaitForElement(_awayFromHomeHeader);
            return this;
        }

        public AwayFromHomePage TapAbsenceReason(string reasonid)
        {
            WaitForElement(_awayFromHomeHeader);
            Tap(_AbsenceReason(reasonid));

            return this;
        }

        public AwayFromHomePage TapAbsencePlannedStartDate()
        {
          
            _app.Tap(_PlannedStartDate);

            return this;
        }

        public AwayFromHomePage TapAbsencePlannedEndDate()
        {

            _app.Tap(_PlannedEndDate);

            return this;
        }

        public AwayFromHomePage TapAbsencePlannedStartTime()
        {

            _app.Tap(_PlannedStartTime);

            return this;
        }

        public AwayFromHomePage TapAbsencePlannedEndTime()
        {

            _app.Tap(_PlannedEndTime);

            return this;
        }

        public AwayFromHomePage setCareNotes(string carenote)
        {

            _app.ClearText(_CareNotesTextAea);
            _app.DismissKeyboard();
            _app.EnterText(_CareNotesTextAea, carenote);
            _app.DismissKeyboard();

            return this;
        }

        public AwayFromHomePage TapSaveNClose()
        {

            _app.Tap(_saveNCloseButton);

            return this;
        }

    }
}
