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
    public class HandoversDetailsPage : CommonMethods
    {

        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _homepageLogo = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _homepageTitle = e => e.Text("HOME");
        Func<AppQuery, AppWebQuery> _HandoverDetailsHeader => e => e.XPath("//div[@id='provider-handover-notes-header']/h2[text()='Unread Handover Notes']");
        Func<AppQuery, AppWebQuery> _UnreadButton(string recordid) => e => e.XPath("//div[@data-id='handover-tile-"+recordid+"']/parent::div/parent::div/div[contains(@data-id,'person-handover-note-unread-chip')]");
        Func<AppQuery, AppWebQuery> _PersonDetails(string personfulname,int number) => e => e.XPath("//div[text()='"+personfulname+" (ID "+number+")']");
        Func<AppQuery, AppWebQuery> _HandoverNotesDailyCareIcon(string dailycare) => e => e.XPath("//h5[text()='"+dailycare+"']");
        Func<AppQuery, AppWebQuery> _HandoverNotesUserInfo(string dataid,string userinfo) => e => e.XPath("//div[@data-id='handover-tile-"+dataid+"']/div/div[text()='"+userinfo+"']");
        Func<AppQuery, AppWebQuery> _HandoverComments(string dataid) => e => e.XPath("//div[@data-id='handover-tile-"+dataid+"']/div/following-sibling::div");
        Func<AppQuery, AppWebQuery> _SeeAllNotesBtn(string recordid) => e => e.XPath("//div[@data-id='handover-tile-"+recordid+ "']/parent::div/following-sibling::div/button[contains(@data-id,'btn-see-all-notes')]");
        Func<AppQuery, AppWebQuery> _MarkAsReadBtn(string recordid) => e => e.XPath("//div[@data-id='handover-tile-"+recordid+"']/parent::div/following-sibling::div/button[contains(@data-id,'btn-mark-as-read')]");

        Func<AppQuery, AppQuery> _WebViewElement= e => e.Class("android.webkit.WebView");



        public HandoversDetailsPage(IApp app)
        {
            this._app = app;
        }

        public HandoversDetailsPage WaitForHandoverDetailsPageToLoad()
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            TryScrollToElement(_homepageTitle);
            CheckIfElementVisible(_homepageTitle);
            WaitForElement(_HandoverDetailsHeader);

            return this;
        }

        public HandoversDetailsPage VerifyHandoverDetails(string recordid,string personfullname, int number,string dailycare)
        {
            WaitForElement(_PersonDetails(personfullname, number));
            ValidateElementVisibility(_UnreadButton(recordid),true);
            ValidateElementVisibility(_HandoverNotesDailyCareIcon(dailycare),true);

            return this;
        }

        public HandoversDetailsPage ValidateHandoversystemuserinfo(string handoverrecordid, string userinfo)
        {
            ValidateElementText(_HandoverNotesUserInfo(handoverrecordid, userinfo), userinfo);

            return this;
        }

        public HandoversDetailsPage ValidateHandoverComments(string handoverrecordid, string expectedcomments)
        {
            ValidateElementText(_HandoverComments(handoverrecordid), expectedcomments);

            return this;
        }

        public HandoversDetailsPage VerifyHandoverSeeAllNotes(string handoverrecordid)
        {
            ValidateElementVisibility(_SeeAllNotesBtn(handoverrecordid), true);

            return this;
        }

        public HandoversDetailsPage TapHandoverSeeAllNotes(string handoverrecordid)
        {
            _app.Tap(_SeeAllNotesBtn(handoverrecordid));

            return this;
        }

        public HandoversDetailsPage VerifyHandoverMarkAsRead(string handoverrecordid)
        {
            ValidateElementVisibility(_MarkAsReadBtn(handoverrecordid), true);

            return this;
        }

    }
}
