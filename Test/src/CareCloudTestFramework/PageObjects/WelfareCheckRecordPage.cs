using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;
using System.Security.Policy;

namespace CareCloudTestFramework.PageObjects
{
    public class WelfareCheckRecordPage : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppWebQuery> _saveNCloseButton = e => e.XPath("//button/span[text()='Save and close']");
        readonly Func<AppQuery, AppWebQuery> _cancelButton = e => e.XPath("//button/span[text()='Cancel']");

        Func<AppQuery, AppWebQuery> _PersonInfoHeader(string personfullname, int number) => e => e.XPath("//span[text()=' " + personfullname + " (ID " + number + ")']");
        Func<AppQuery, AppWebQuery> _WelFareCheckHeader => e => e.XPath("//div[text()='Welfare Check']");
        Func<AppQuery, AppWebQuery> _StatusRadioBtn(string status) => e => e.XPath("//input[@text='" + status + "']/parent::label");
        Func<AppQuery, AppWebQuery> _ObservationsBtn => e => e.XPath("//*[@id='add-welfare-check-observations']");
        Func<AppQuery, AppWebQuery> _specicfyOtherObservationTxtFld => e => e.XPath("//*[@id='welfare-check-other-observation-text']");
        Func<AppQuery, AppWebQuery> _DetailsOfConversationTxtFld => e => e.XPath("//textarea[@id='welfare-check-details-of-conversation-text']");
        Func<AppQuery, AppWebQuery> _WelfareCheckLocationRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "']/parent::label");
        Func<AppQuery, AppWebQuery> _IsResedentInSafeLocation(string option) => e => e.XPath("//input[@text='" + option + "' and @name='isTheResidentInASafePlace']/parent::label");
        Func<AppQuery, AppWebQuery> _ActionTakenTxtFld => e => e.XPath("//textarea[@id='ActionTakenSafePlaceActionText']");
        Func<AppQuery, AppWebQuery> _WelfareOccuredtimefld => e => e.XPath("//*[@id='welfare-check-occurred-time']");
        Func<AppQuery, AppWebQuery> _Add5MinTimeSpentButton => e => e.XPath("//*[@id='add-5min-time-spent-btn']");
        Func<AppQuery, AppWebQuery> _CareNotesTxtFld => e => e.XPath("//*[@id='careNotes']");
        Func<AppQuery, AppWebQuery> _FlagRecordForHandoverRadioBtn(int value) => e => e.XPath("//input[@value='" + value + "' and @name='flagRecordForHandover']/parent::label");
        Func<AppQuery, AppWebQuery> _HandoverComentsTxtFld => e => e.XPath("//*[@id='welfare-check-handover-comments']");
        Func<AppQuery, AppWebQuery> _WellBeingRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='welfare-check-care-wellbeing']/parent::label");
        Func<AppQuery, AppWebQuery> _TimeCareGivenValidation1 => e => e.XPath("//div[@id='welfare-check-occurred-error']/span[text()='You cannot select future time.']");
        Func<AppQuery, AppWebQuery> _TimeCareGivenValidation2 => e => e.XPath("//div[@id='welfare-check-occurred-2-error']/span[text()='Time care was given and time spent with person cannot be greater than the current time when combined.']");
        Func<AppQuery, AppWebQuery> _TimeSpentValidation => e => e.XPath("//div[@id='welfare-check-time-spent-with-client-2-error']/span[text()='Time care was given and time spent with person cannot be greater than the current time when combined.']");

        Func<AppQuery, AppQuery> _WebViewElement = e => e.Class("android.webkit.WebView");



        public WelfareCheckRecordPage(IApp app)
        {
            _app = app;

        }



        public WelfareCheckRecordPage WaitForWelfareCheckRecordPageLookupToLoad(string fullname, int number)
        {
            //WaitForElement(_saveNCloseButton);
            //WaitForElement(_cancelButton);
            WaitForElement(_PersonInfoHeader(fullname, number));
            WaitForElement(_WelFareCheckHeader);

            return this;
        }

        public WelfareCheckRecordPage WaitForWelfareCheckRecordPageToReLoad(string fullname, int number)
        {
            //WaitForElement(_saveNCloseButton);
            //WaitForElement(_cancelButton);
            WaitForElement(_PersonInfoHeader(fullname, number));
            

            return this;
        }


        public WelfareCheckRecordPage TapWelFareChKStatsButton(string status)
        {
            _app.Tap(_StatusRadioBtn(status));

            return this;
        }

        public WelfareCheckRecordPage TapWelFareChKObservationButton()
        {
            _app.Tap(_ObservationsBtn);

            return this;
        }

        public WelfareCheckRecordPage InsertSpecifyOtherConversationTextBox(string OtherConversationTxtFld)
        {
            _app.ClearText(_specicfyOtherObservationTxtFld);
            _app.DismissKeyboard();

            _app.EnterText(_specicfyOtherObservationTxtFld, OtherConversationTxtFld);
            _app.DismissKeyboard();

            return this;
        }

        public WelfareCheckRecordPage InsertDetailsOfConversationTextBox(string detailsOfConversationTxtFld)
        {
            _app.ClearText(_DetailsOfConversationTxtFld);
            _app.DismissKeyboard();

            _app.EnterText(_DetailsOfConversationTxtFld, detailsOfConversationTxtFld);
            _app.DismissKeyboard();

            return this;
        }

        public WelfareCheckRecordPage TapIsThisResSafeRadioButton(string option)
        {
            _app.Tap(_IsResedentInSafeLocation(option));

            return this;
        }

        public WelfareCheckRecordPage TapLocationButton(string dataid)
        {
            ScrollDownWithinElement(_WelfareCheckLocationRadioBtn(dataid), _WebViewElement);
            _app.Tap(_WelfareCheckLocationRadioBtn(dataid));

            return this;
        }

        public WelfareCheckRecordPage TapWellbeingButton(string dataid)
        {
            ScrollDownWithinElement(_WellBeingRadioBtn(dataid), _WebViewElement);
            _app.Tap(_WellBeingRadioBtn(dataid));

            return this;
        }


        public WelfareCheckRecordPage InsertTimeCareGiven()
        {
            ScrollDownWithinElement(_WelfareOccuredtimefld, _WebViewElement);
            _app.Tap(_WelfareOccuredtimefld);

            return this;
        }

        public WelfareCheckRecordPage InsertTimeSpent()
        {
            ScrollDownWithinElement(_Add5MinTimeSpentButton, _WebViewElement);
            _app.Tap(_Add5MinTimeSpentButton);

            return this;
        }

        public WelfareCheckRecordPage TapsaveNClose()
        {
            ScrollDownWithinElement(_saveNCloseButton, _WebViewElement);
            _app.Tap(_saveNCloseButton);

            return this;
        }

        public WelfareCheckRecordPage validateTimeCareGivenValidation1(string ExpectText)
        {

            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeCareGivenValidation1, ExpectText);

            return this;

        }

        public WelfareCheckRecordPage validateTimeCareGivenValidation2(string ExpectText)
        {

            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeCareGivenValidation2, ExpectText);

            return this;

        }

        public WelfareCheckRecordPage validateTimeSpentValidation(string ExpectText)
        {
            System.Threading.Thread.Sleep(1000);
            ScrollDownWithinElement(_CareNotesTxtFld, _WebViewElement);
            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeSpentValidation, ExpectText);

            return this;

        }

    }
}
