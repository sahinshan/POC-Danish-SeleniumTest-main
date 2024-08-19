using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CareCloudTestFramework.PageObjects
{
    public class MainMenu : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.All().Marked("MenuBtn");

        readonly Func<AppQuery, AppQuery> _peopleLink = e => e.Marked("Workplace_Item_PEOPLE");
        readonly Func<AppQuery, AppQuery> _homeLink = e => e.Marked("Home");
        readonly Func<AppQuery, AppQuery> _appointmentsLink = e => e.Marked("Workplace_Item_APPOINTMENTS");

        readonly Func<AppQuery, AppQuery> _serviceEndpointsLink = e => e.Marked("SERVICE ENDPOINTS");
        readonly Func<AppQuery, AppQuery> _aboutLink = e => e.Marked("AboutLabel");
        readonly Func<AppQuery, AppQuery> _workPlaceLink = e => e.Marked("WORKPLACE");
        readonly Func<AppQuery, AppQuery> _logoutLink = e => e.Marked("MenuContainer").Descendant().Marked("LogoutLabel");
        readonly Func<AppQuery, AppQuery> _okButton = e => e.Id("button2").Text("OK");
        readonly Func<AppQuery, AppQuery> _settingsLink = e => e.Marked("MenuContainer").Descendant().Marked("SettingsLabel");
        readonly Func<AppQuery, AppQuery> _careCloudOptionsLabel = e => e.Marked("DefaultOptionsLabel").Text("CARECLOUD");
        Func<AppQuery, AppQuery> _record(string RecordID) => e => e.WebView().Id(RecordID);
        Func <AppQuery, AppQuery> _carecloudLoggedInUserName(string userName) => e => e.Text(userName);

        private IApp app;
        private ErrorPopUp errorpopup;


        public MainMenu(IApp app)
        {
            _app = app;

        }

        public MainMenu WaitForMainMenuButtonToLoad()
        {
            _app.WaitForElement(_mainMenuButton);

            return this;
        }




        public ServiceEndpointsPage NavigateToServiceEndpointsLink()
        {
            _app.Tap(_mainMenuButton);

            //if the error pop-up is open close it
            bool okButtonVisible = CheckIfElementVisible(_okButton); ;

            if (!okButtonVisible)
                _app.Tap(_serviceEndpointsLink);
            else
            {
                _app.Tap(_okButton);
                _app.Tap(_serviceEndpointsLink);
            }
            _app.WaitForElement(_serviceEndpointsLink);
            
            if (okButtonVisible)
                _app.Tap(_okButton);
            else _app.WaitForElement(_serviceEndpointsLink);

            if (okButtonVisible)
                _app.Tap(_okButton);
            else _app.WaitForElement(_serviceEndpointsLink);
           
            return new ServiceEndpointsPage(_app);
        }

        public SettingsPage NavigateToSettingsPage(String UserName)
        {

            this.Tap(_mainMenuButton);
            this.WaitForElement(_carecloudLoggedInUserName(UserName));

            bool settingsLinkVisible = this.CheckIfElementVisible(_settingsLink);
            if (!settingsLinkVisible)
                this.Tap(_careCloudOptionsLabel);

            this.Tap(_settingsLink);

            return new SettingsPage(_app);

           
        }

        public MainMenu NavigateToHomePage()
        {
            _app.Tap(_mainMenuButton);
            _app.WaitForElement(_homeLink);
            _app.Tap(_homeLink);

            return this;
        }

        public MainMenu clickrecord(String recordid)
        {
            //_app.Tap(_record(recordid));
            _app.Tap(x => x.XPath("//*[@data-id='42d68727-f910-ee11-8428-0a26d90f0ccc']"));
           // _app.Tap(x => x.XPath("//button[@id='start-visit-button']"));

            return this;
        }

    }

}
