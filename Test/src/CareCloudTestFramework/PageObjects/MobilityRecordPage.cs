using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;
using System.Security.Policy;
using System.Runtime.InteropServices.ComTypes;

namespace CareCloudTestFramework.PageObjects
{
    public class MobilityRecordPage : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppWebQuery> _saveNCloseButton = e => e.XPath("//button/span[text()='Save and close']");
        readonly Func<AppQuery, AppWebQuery> _cancelButton = e => e.XPath("//button/span[text()='Cancel']");

        Func<AppQuery, AppWebQuery> _PersonInfoHeader(string personfullname, int number) => e => e.XPath("//span[text()=' " + personfullname + " (ID " + number + ")']");
        Func<AppQuery, AppWebQuery> _MobiltyHeader => e => e.XPath("//div[text()='Mobility']");
        Func<AppQuery, AppWebQuery> _ConsentGivenHeader => e => e.XPath("//div[text()='Consent given']");

        Func<AppQuery, AppWebQuery> _MobilisedFromRadioBtn(string dataid) => e => e.XPath("//input[@value='"+dataid+ "' and @name='mobility-location-from']/parent::label");
        Func<AppQuery, AppWebQuery> _MobilisedToRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='mobility-location-to']/parent::label");
        Func<AppQuery, AppWebQuery> _ApproxDistanceUnitRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='approximateDistanceUnit']/parent::label");
        Func<AppQuery, AppWebQuery> _ApproxDistanceFld => e => e.XPath("//input[@id='mobility-approximate-distance']");
        Func<AppQuery, AppWebQuery> _EquipmentRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='mobility-care-equipment-list']/parent::label");
        Func<AppQuery, AppWebQuery> _WellBeingRadioBtn(string dataid) => e => e.XPath("//label[@data-id='label-" + dataid + "']");
        Func<AppQuery, AppWebQuery> _AssistanceNeededRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='mobility-care-assistance-needed']/parent::label");
        Func<AppQuery, AppWebQuery> _MobilityStaffRequiredAddBtn => e => e.XPath("//*[@id='add-mobilityStaffRequired']");
        Func<AppQuery, AppWebQuery> _Occuredtimefld => e => e.XPath("//*[@id='mobility-occurred-time']");
        Func<AppQuery, AppWebQuery> _Add5MinTimeSpentButton=> e => e.XPath("//*[@id='add-5min-time-spent-btn']");
        Func<AppQuery, AppWebQuery> _AdditionalNotesTxtFld => e => e.XPath("//*[@id='mobility-additional-notes-text']");
        Func<AppQuery, AppWebQuery> _CareNotesTxtFld => e => e.XPath("//*[@id='careNotes']");
        Func<AppQuery, AppWebQuery> _FlagRecordForHandoverRadioBtn(int value) => e => e.XPath("//input[@value='" + value + "' and @name='flagRecordForHandover']/parent::label");
        Func<AppQuery, AppWebQuery> _HandoverComentsTxtFld => e => e.XPath("//*[@id='careNotes']");
        Func<AppQuery, AppWebQuery> _TimeCareGivenValidation1 => e => e.XPath("//div[@id='mobility-occurred-error']/span[text()='You cannot select future time.']");
        Func<AppQuery, AppWebQuery> _TimeCareGivenValidation2 => e => e.XPath("//div[@id='mobility-occurred-2-error']/span[text()='Time care was given and time spent with person cannot be greater than the current time when combined.']");
        Func<AppQuery, AppWebQuery> _TimeSpentValidation => e => e.XPath("//div[@id='mobility-time-spent-with-client-2-error']/span[text()='Time care was given and time spent with person cannot be greater than the current time when combined.']");

        Func<AppQuery, AppQuery> _WebViewElement = e => e.Class("android.webkit.WebView");


        public MobilityRecordPage(IApp app)
        {
            _app = app;

        }



        public MobilityRecordPage WaitForMobilityRecordPageLookupToLoad(string fullname,int number)
        {
            //WaitForElement(_saveNCloseButton);
            //WaitForElement(_cancelButton);
            WaitForElement(_PersonInfoHeader(fullname, number));
            WaitForElement(_MobiltyHeader);
            WaitForElement(_ConsentGivenHeader);

            return this;
        }

        public MobilityRecordPage WaitForMobilityRecordPageLookupToReLoad(string fullname, int number)
        {
            //WaitForElement(_saveNCloseButton);
            //WaitForElement(_cancelButton);
            WaitForElement(_PersonInfoHeader(fullname, number));
          
            return this;
        }


        public MobilityRecordPage TapMobilisedFromButton(string dataid)
        {
            _app.Tap(_MobilisedFromRadioBtn(dataid));

            return this;
        }

        public MobilityRecordPage TapMobilisedToButton(string dataid)
        {
            _app.Tap(_MobilisedToRadioBtn(dataid));

            return this;
        }

        public MobilityRecordPage InsertApproxiamateDistUnitTextBox(string distance)
        {
            _app.ClearText(_ApproxDistanceFld);
            _app.DismissKeyboard();

            _app.EnterText(_ApproxDistanceFld, distance);
            _app.DismissKeyboard();

            return this;
        }

        public MobilityRecordPage TapEquipmentButton(string dataid)
        {
            _app.Tap(_EquipmentRadioBtn(dataid));

            return this;
        }

        public MobilityRecordPage TapWellbeingButton(string dataid)
        {
            ScrollDownWithinElement(_WellBeingRadioBtn(dataid), _WebViewElement);
            _app.Tap(_WellBeingRadioBtn(dataid));

            return this;
        }

        public MobilityRecordPage TapAssistanceButton(string dataid)
        {
            ScrollDownWithinElement(_AssistanceNeededRadioBtn(dataid), _WebViewElement);
            _app.Tap(_AssistanceNeededRadioBtn(dataid));

            return this;
        }

        public MobilityRecordPage InsertTimeCareGiven()
        {
            ScrollDownWithinElement(_Occuredtimefld, _WebViewElement);
            _app.Tap(_Occuredtimefld);

            return this;
        }

        public MobilityRecordPage InsertTimeSpent()
        {
            ScrollDownWithinElement(_Add5MinTimeSpentButton, _WebViewElement);
            _app.Tap(_Add5MinTimeSpentButton);

            return this;
        }

        public MobilityRecordPage TapsaveNClose()
        {
            ScrollDownWithinElement(_saveNCloseButton, _WebViewElement);
            _app.Tap(_saveNCloseButton);

            return this;
        }

        public MobilityRecordPage validateTimeCareGivenValidation1(string ExpectText)
        {

            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeCareGivenValidation1, ExpectText);

            return this;

        }

        public MobilityRecordPage validateTimeCareGivenValidation2(string ExpectText)
        {

            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeCareGivenValidation2, ExpectText);

            return this;

        }

        public MobilityRecordPage validateTimeSpentValidation(string ExpectText)
        {
            System.Threading.Thread.Sleep(1000);
            ScrollDownWithinElement(_AdditionalNotesTxtFld, _WebViewElement);
            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeSpentValidation, ExpectText);

            return this;

        }
    }
}
