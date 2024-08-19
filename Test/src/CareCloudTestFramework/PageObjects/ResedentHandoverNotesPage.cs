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
    public class ResedentHandoverNotesPage : CommonMethods
    {

        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _homepageLogo = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _homepageTitle = e => e.Text("HOME");
        Func<AppQuery, AppWebQuery> _ResHandoverDetailsHeader => e => e.XPath("//div[@id='handover-notes-order-message']");
        Func<AppQuery, AppWebQuery> _HandoverNotesDailyCareIcon(string dailycare) => e => e.XPath("//h5[text()='" + dailycare + "']");
        Func<AppQuery, AppWebQuery> _HandoverNotesUserInfo(string dataid, string userinfo) => e => e.XPath("//div[@data-id='handover-tile-" + dataid + "']/div/div[text()='" + userinfo + "']");
        Func<AppQuery, AppWebQuery> _HandoverComments(string dataid) => e => e.XPath("//div[@data-id='handover-tile-" + dataid + "']/div/following-sibling::div");
        Func<AppQuery, AppWebQuery> _AddNotesBtn(string recordid) => e => e.XPath("//button[@data-id='btn-add-handover-note-" + recordid + "']");
        Func <AppQuery, AppWebQuery> _MarkAsReadBtn(string recordid) => e => e.XPath("//button[@data-id='btn-mark-as-read-"+recordid+"']");
        Func<AppQuery, AppQuery> _WebViewElement= e => e.Class("android.webkit.WebView");



        public ResedentHandoverNotesPage(IApp app)
        {
            this._app = app;
        }

        public ResedentHandoverNotesPage WaitForResedentHandoverNotesPageToLoad()
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            TryScrollToElement(_homepageTitle);
            CheckIfElementVisible(_homepageTitle);
            WaitForElement(_ResHandoverDetailsHeader);

            return this;
        }

        public ResedentHandoverNotesPage VerifyResHandoverDetails(string dailycare)
        {
            
            ValidateElementVisibility(_HandoverNotesDailyCareIcon(dailycare), true);

            return this;
        }


        public ResedentHandoverNotesPage ValidateResHandoversystemuserinfo(string handoverrecordid, string userinfo)
        {
            ValidateElementText(_HandoverNotesUserInfo(handoverrecordid, userinfo), userinfo);

            return this;
        }

        public ResedentHandoverNotesPage ValidateResHandoverComments(string handoverrecordid, string expectedcomments)
        {
            ValidateElementText(_HandoverComments(handoverrecordid), expectedcomments);

            return this;
        }

        public ResedentHandoverNotesPage VerifyResHandoverAddNotes(string handoverrecordid)
        {
            ValidateElementVisibility(_AddNotesBtn(handoverrecordid), true);

            return this;
        }

        public ResedentHandoverNotesPage TapResHandoverAddNotes(string handoverrecordid)
        {
            _app.Tap(_AddNotesBtn(handoverrecordid));

            return this;
        }

        public ResedentHandoverNotesPage VerifyResHandoverMarkAsRead(string handoverrecordid)
        {
            ValidateElementVisibility(_MarkAsReadBtn(handoverrecordid), true);

            return this;
        }

    }
}
