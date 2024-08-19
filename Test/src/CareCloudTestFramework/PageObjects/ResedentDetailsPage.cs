using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;
using System.Runtime.InteropServices.ComTypes;

namespace CareCloudTestFramework.PageObjects
{
    public class ResedentDetailsPage : CommonMethods
    {

        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _homepageLogo = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _homepageTitle = e => e.Text("HOME");
        Func<AppQuery, AppWebQuery> _PersonInfoHeader(string personfullname, int number) => e => e.XPath("//span[text()=' " + personfullname + " (ID " + number + ")']");
        Func<AppQuery, AppWebQuery> _CareTab => e => e.XPath("//li/label/span[text()='Care']");
        Func<AppQuery, AppWebQuery> _AddCareButton => e => e.XPath("//*[@id='add-non-scheduled-daily-care']");
        Func<AppQuery, AppWebQuery> _BOButton(string BusinessObject) => e => e.XPath("//div/div//h3[text()='" + BusinessObject + "']");
        Func<AppQuery, AppWebQuery> _ScheduledBOButton => e => e.XPath("//div[@id='care-items-timeline']/div");
        Func<AppQuery, AppWebQuery> _ConsentGivenYesRadio => e => e.XPath("//div[@id='input-list-container']/label[@data-id='label-1']");
        Func<AppQuery, AppWebQuery> _ConsentGivenNoRadio => e => e.XPath("//div[@id='input-list-container']/label[@data-id='label-2']");

        Func<AppQuery, AppQuery> _WebViewElement = e => e.Class("android.webkit.WebView");
        Func<AppQuery, AppWebQuery> _ResedentInfoExpanButton => e => e.XPath("//span[text()='Resident Information']");
        Func<AppQuery, AppWebQuery> _KeyRiskHeader => e => e.XPath("//div[@id='residential-additional-info-container']//h6[text()='Key Risks']");
        Func<AppQuery, AppWebQuery> _ViewAllRisksButton => e => e.XPath("//*[@id='view-all-risks-btn']");
        Func<AppQuery, AppWebQuery> _CompletedRecord(string dataid) => e => e.XPath("//div[@linked-record-id='" + dataid + "']");
        Func<AppQuery, AppWebQuery> __CareNoteText => e => e.XPath("//div[@id='daily-care-content']/child::div");
        Func<AppQuery, AppWebQuery> _MobilityHeader(string dataid) => e => e.XPath("//div[@data-id='" + dataid + "']/span/following-sibling::div/h5[text()='Mobility']");
        Func<AppQuery, AppWebQuery> _ConsentDetails(string dataid, string consent) => e => e.XPath("//div[@data-id='" + dataid + "']/span/following-sibling::div/div//span[text()='" + consent + "']");
        Func<AppQuery, AppWebQuery> _ConsentDeclinedHeader => e => e.XPath("//div[text()='Consent declined']");
        Func<AppQuery, AppWebQuery> _ConsentFormAbsent => e => e.XPath("//label[@data-id='label-1']");
        Func<AppQuery, AppWebQuery> _ViewCarePlanButton => e => e.XPath("//span[text()='View Care Plan']");

        Func<AppQuery, AppWebQuery> _ConsentFormDeclined => e => e.XPath("//label[@data-id='label-2']");
        Func<AppQuery, AppWebQuery> _ConsentFormDeferred => e => e.XPath("//label[@data-id='label-3']");

        Func<AppQuery, AppWebQuery> _ReasonForAbsenceHeader => e => e.XPath("//div[@data-id='no-consent-absent-reason-label']");
        Func<AppQuery, AppWebQuery> _ReasonForAbsenceText => e => e.XPath("//*[@id='no-consent-absent-reason']");
        readonly Func<AppQuery, AppWebQuery> _saveNCloseButton = e => e.XPath("//button/span[text()='Save and close']");
        readonly Func<AppQuery, AppWebQuery> _cancelButton = e => e.XPath("//button/span[text()='Cancel']");
        Func<AppQuery, AppWebQuery> _DeclinedHeader => e => e.XPath("//div[text()='Declined']");

        Func<AppQuery, AppWebQuery> _DeferreddHeader => e => e.XPath("//div[text()='Deferred']");

        Func<AppQuery, AppWebQuery> _PickDateNTime => e => e.XPath("//div[@id='non-consent-deferred']//label[@data-id='label-2']");
        Func<AppQuery, AppWebQuery> _DeferredTomorrow => e => e.XPath("//input[@text='Tomorrow']/parent::label");
        Func<AppQuery, AppWebQuery> _PickTime => e => e.XPath("//span[text()='Pick time']");
        Func<AppQuery, AppWebQuery> _TimeFld => e => e.XPath("//input[@id='non-consent-deferred-to-time']");


        Func<AppQuery, AppWebQuery> _ReasonForDecliningText => e => e.XPath("//*[@id='no-consent-declined-reason']");

        Func<AppQuery, AppWebQuery> _EncourageResidentText => e => e.XPath("//*[@id='no-consent-declined-encourage']");

        Func<AppQuery, AppWebQuery> _ReasonForDeclining => e => e.XPath("//div[@id='daily-care-content']/child::div/div[1]/div");
        Func<AppQuery, AppWebQuery> _EncouragementGiven => e => e.XPath("//div[@id='daily-care-content']/child::div/div[2]/div");
        Func<AppQuery, AppWebQuery> _DeferredToHeader => e => e.XPath("//div[@id='daily-care-content']/h5");
        Func<AppQuery, AppWebQuery> _DeferredToDate => e => e.XPath("//div[@id='daily-care-content']/span");
        Func<AppQuery, AppWebQuery> _DeferredToTime => e => e.XPath("//div[@id='daily-care-content']/child::div/span");
        Func<AppQuery, AppWebQuery> _HandoverNotesTab => e => e.XPath("//li/label/span[text()='Handover Notes']");


        public ResedentDetailsPage(IApp app)
        {
            this._app = app;
        }
        public ResedentDetailsPage WaitForResedentDetailsPageToLoad(string personfullname, int number)
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            TryScrollToElement(_homepageTitle);
            CheckIfElementVisible(_homepageTitle);
            WaitForElement(_PersonInfoHeader(personfullname, number));
            System.Threading.Thread.Sleep(1000);

            return this;
        }

        public ResedentDetailsPage TapCareTab()
        {

            Tap(_CareTab);

            return this;
        }

        public ResedentDetailsPage TapHandOverNotesTab()
        {

            Tap(_HandoverNotesTab);

            return this;
        }


        public ResedentDetailsPage TapAddCareButton()
        {

            Tap(_AddCareButton);

            return this;
        }

        public ResedentDetailsPage TapBO(string personfullname, int number, string BusinessObject)
        {
            ScrollDownWithinElement(_BOButton(BusinessObject), _WebViewElement);
            WaitForElement(_PersonInfoHeader(personfullname, number));
            Tap(_BOButton(BusinessObject));

            return this;
        }

        public ResedentDetailsPage TapScheduledBO(string personfullname, int number)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            Tap(_ScheduledBOButton);

            return this;
        }

        public ResedentDetailsPage TapAwayFromHomeBO(string personfullname, int number, string BusinessObject)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            Tap(_BOButton(BusinessObject));

            return this;
        }

        public ResedentDetailsPage TapContinenceCareBO(string personfullname, int number, string BusinessObject)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            Tap(_BOButton(BusinessObject));

            return this;
        }

        public ResedentDetailsPage TapCareConsentgivenYes(string personfullname, int number)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            Tap(_ConsentGivenYesRadio);

            return this;
        }

        public ResedentDetailsPage TapCareConsentgivenNo(string personfullname, int number)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            Tap(_ConsentGivenNoRadio);

            return this;
        }

        public ResedentDetailsPage TapCareConsentAbsent(string personfullname, int number)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            WaitForElement(_ConsentDeclinedHeader);
            Tap(_ConsentFormAbsent);

            return this;
        }

        public ResedentDetailsPage TapCareConsentDeclined(string personfullname, int number)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            WaitForElement(_ConsentDeclinedHeader);
            Tap(_ConsentFormDeclined);

            return this;
        }

        public ResedentDetailsPage TapCareConsentDeferred(string personfullname, int number)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            WaitForElement(_ConsentDeclinedHeader);
            Tap(_ConsentFormDeferred);

            return this;
        }


        public ResedentDetailsPage setCareConsentAbsent(string personfullname, int number, string absencereason)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            WaitForElement(_ReasonForAbsenceHeader);
            _app.ClearText(_ReasonForAbsenceText);
            _app.DismissKeyboard();

            _app.EnterText(_ReasonForAbsenceText, absencereason);
            _app.DismissKeyboard();

            return this;
        }

        public ResedentDetailsPage setReasonCareConsenDeclined(string personfullname, int number, string ReasonForDeclining)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            WaitForElement(_DeclinedHeader);
            _app.ClearText(_ReasonForDecliningText);
            _app.DismissKeyboard();

            _app.EnterText(_ReasonForDecliningText, ReasonForDeclining);
            _app.DismissKeyboard();

            return this;
        }

        public ResedentDetailsPage setReasonCareConsenDeferred(string personfullname, int number)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            WaitForElement(_DeferreddHeader);
            Tap(_DeferredTomorrow);
            Tap(_PickTime);
            Tap(_TimeFld);



            return this;
        }

        public ResedentDetailsPage setEncourageConsentDeclined(string personfullname, int number, string encourageresidentText)
        {
            WaitForElement(_PersonInfoHeader(personfullname, number));
            WaitForElement(_DeclinedHeader);
            _app.ClearText(_EncourageResidentText);
            _app.DismissKeyboard();

            _app.EnterText(_EncourageResidentText, encourageresidentText);
            _app.DismissKeyboard();

            return this;
        }

        public ResedentDetailsPage TapResedentInformationExpandButton()
        {

            Tap(_ResedentInfoExpanButton);

            return this;
        }

        public ResedentDetailsPage TapViewCarePlanButton()
        {

            Tap(_ViewCarePlanButton);

            return this;
        }

        public ResedentDetailsPage VerfiyKeyRiskLabel()
        {

            CheckIfElementVisible(_KeyRiskHeader);
            CheckIfElementVisible(_ViewAllRisksButton);

            return this;
        }

        public ResedentDetailsPage TapViewAllRisksButton()
        {

            Tap(_ViewAllRisksButton);

            return this;
        }

        public ResedentDetailsPage VerifyConsentAgreed(string dataid, string consent)
        {

            CheckIfElementVisible(_MobilityHeader(dataid));
            CheckIfElementVisible(_ConsentDetails(dataid, consent));

            return this;
        }

        public ResedentDetailsPage TapCompletedRecord(string dataid)
        {

            Tap(_CompletedRecord(dataid));

            return this;
        }

        public ResedentDetailsPage validateCareNote(string ExpectText)
        {

            //string fieldText = GetWebElementText(__CareNoteText);
            // Assert.AreEqual(ExpectText, fieldText);
            System.Threading.Thread.Sleep(1000);
            ValidateElementText(__CareNoteText, ExpectText);

            return this;

        }

        public ResedentDetailsPage validateCareDecline(string DeclineExpectText, string EncouragementGivenExpectText)
        {


            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_ReasonForDeclining, DeclineExpectText);
            ValidateElementText(_EncouragementGiven, EncouragementGivenExpectText);


            return this;

        }

        public ResedentDetailsPage validateCareDeferred(string DeferredToHeader, string date, string time)
        {


            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_DeferredToHeader, DeferredToHeader);
            ValidateElementText(_DeferredToDate, date);
            ValidateElementText(_DeferredToTime, time);


            return this;

        }


        public ResedentDetailsPage TapsaveNClose()
        {
            ScrollDownWithinElement(_saveNCloseButton, _WebViewElement);
            _app.Tap(_saveNCloseButton);

            return this;
        }

    }
}
