using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;
using System.Runtime.Remoting.Messaging;

namespace CareCloudTestFramework.PageObjects
{
    public class SeeAllNotesPage : CommonMethods
    {

        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _homepageLogo = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _homepageTitle = e => e.Text("HOME");
        Func<AppQuery, AppWebQuery> _SeeAllNotesHeader => e => e.XPath("//div[@id='resident-handover-notes-title']/h2[text()='Handover Notes']");
        Func<AppQuery, AppWebQuery> _SeeAllNotesSubHeader => e => e.XPath("//div[@id='resident-handover-notes-title']/div[text()='Showing all notes chronologically']");
        Func<AppQuery, AppWebQuery> _PersonDetails(string personfulname, int number) => e => e.XPath("//div[text()='" + personfulname + " (ID " + number + ")']");
        Func<AppQuery, AppWebQuery> _HandoverNotesDailyCareIcon(string dailycare) => e => e.XPath("//div[@id='resident-handover-notes-container']//h5[text()='"+dailycare+"']");
        Func<AppQuery, AppWebQuery> _SeeAllNotesUserInfo(string dataid, string userinfo) => e => e.XPath("//div[@data-id='handover-tile-" + dataid + "']/div/div[text()='" + userinfo + "']");
        Func<AppQuery, AppWebQuery> _HandoverComments(string dataid) => e => e.XPath("//div[@data-id='handover-tile-" + dataid + "']/div/following-sibling::div");
        Func<AppQuery, AppWebQuery> _AddNotesButton(string dataid) => e => e.XPath("//div[@data-id='handover-tile-" + dataid + "']/following-sibling::div/div/button[@data-id='btn-add-handover-note-" + dataid + "']");
        Func<AppQuery, AppWebQuery> _MarkAsReadButton(string dataid) => e => e.XPath("//div[@data-id='handover-tile-"+dataid+"']/following-sibling::div//button[contains(@data-id,'btn-mark-as-read-')]");
        Func<AppQuery, AppWebQuery> _SaveHandoverNoteBtn => e => e.XPath("//button[@id='save-handover-note-btn']");
        Func<AppQuery, AppWebQuery> _CancelHandoverNoteBtn => e => e.XPath("//button[@id='cancel-handover-note-btn']");
        Func<AppQuery, AppWebQuery> _HandoverTextArea => e => e.XPath("//*[@id='new-handover-textarea']");
        Func<AppQuery, AppWebQuery> _ErrorMessage(string Text) => e => e.XPath("//div[@id='new-handover-textarea-error']/span[text()='"+Text+"']");

        Func<AppQuery, AppWebQuery> _ResPersonDetails(string personfulname, int number) => e => e.XPath("//span[text()='" + personfulname + " (ID " + number + ")']");

        Func<AppQuery, AppQuery> _WebViewElement= e => e.Class("android.webkit.WebView");



        public SeeAllNotesPage(IApp app)
        {
            this._app = app;
        }

        public SeeAllNotesPage WaitForSeeAllNotesPageToLoad()
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            TryScrollToElement(_homepageTitle);
            CheckIfElementVisible(_homepageTitle);
            WaitForElement(_SeeAllNotesHeader);
            WaitForElement(_SeeAllNotesSubHeader);

            return this;
        }

        public SeeAllNotesPage VerifySeeAllNotesPage(string recordid,string personfullname, int number,string dailycare)
        {
            WaitForElement(_PersonDetails(personfullname, number));
            ValidateElementVisibility(_HandoverNotesDailyCareIcon(dailycare),true);

            return this;
        }

        public SeeAllNotesPage ValidateSeeAllNotessystemuserinfo(string handoverrecordid, string userinfo)
        {
            ValidateElementText(_SeeAllNotesUserInfo(handoverrecordid, userinfo), userinfo);

            return this;
        }

        public SeeAllNotesPage ValidateSeeAllNotesComments(string handoverrecordid, string expectedcomments)
        {
            ValidateElementText(_HandoverComments(handoverrecordid), expectedcomments);

            return this;
        }

        public SeeAllNotesPage VerifyHandoverAddNotes(string handoverrecordid)
        {
            ValidateElementVisibility(_AddNotesButton(handoverrecordid), true);

            return this;
        }

        public SeeAllNotesPage TapHandoverAddNotes(string handoverrecordid)
        {
            _app.Tap(_AddNotesButton(handoverrecordid));

            return this;
        }

        public SeeAllNotesPage VerifyMarkAsRead(string handoverrecordid)
        {
            ValidateElementVisibility(_MarkAsReadButton(handoverrecordid), true);

            return this;
        }

        public SeeAllNotesPage WaitforNewHandoverNotesToLoad(string personfullname, int number)
        {
            WaitForElement(_PersonDetails(personfullname, number));
            WaitForElement(_SaveHandoverNoteBtn);
            WaitForElement(_CancelHandoverNoteBtn);
      
            return this;
        }

        public SeeAllNotesPage WaitforNewResHandoverNotesToLoad(string personfullname, int number)
        {
            WaitForElement(_ResPersonDetails(personfullname, number));
            WaitForElement(_SaveHandoverNoteBtn);
            WaitForElement(_CancelHandoverNoteBtn);

            return this;
        }


        public SeeAllNotesPage SetHandoverNotes(string _Handovercomments)
        {
           
            _app.ClearText(_HandoverTextArea);
            _app.DismissKeyboard();

            _app.EnterText(_HandoverTextArea, _Handovercomments);
            _app.DismissKeyboard();

            return this;
        }

        public SeeAllNotesPage TapSaveHandoveNotes()
        {
            _app.Tap(_SaveHandoverNoteBtn);

            return this;
        }

        public SeeAllNotesPage ValidateErrorMessage(string text)
        {
            ValidateElementText(_ErrorMessage(text), text);

            return this;
        }

        public SeeAllNotesPage TapCancelHandoveNotes()
        {
            _app.Tap(_CancelHandoverNoteBtn);

            return this;
        }


    }
}
