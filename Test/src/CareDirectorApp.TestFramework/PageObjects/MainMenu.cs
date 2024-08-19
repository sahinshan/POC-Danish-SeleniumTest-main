using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;
using System.Collections.Generic;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class MainMenu: CommonMethods
    {
        readonly Func<AppQuery, AppQuery> _mainMenuButton1 = e => e.All().Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");

        readonly Func<AppQuery, AppQuery> _homeLink = e => e.Marked("Workplace_Item_HOME");
        readonly Func<AppQuery, AppQuery> _peopleLink = e => e.Marked("Workplace_Item_PEOPLE");
        readonly Func<AppQuery, AppQuery> _casesLink = e => e.Marked("Workplace_Item_CASES");
        readonly Func<AppQuery, AppQuery> _appointmentsLink = e => e.Marked("Workplace_Item_APPOINTMENTS");

        readonly Func<AppQuery, AppQuery> _serviceEndpointsLink = e => e.Marked("EnvironmentsLabel");
        readonly Func<AppQuery, AppQuery> _aboutLink = e => e.Marked("AboutLabel");
        readonly Func<AppQuery, AppQuery> _settingsLink = e => e.Marked("MenuContainer").Descendant().Marked("SettingsLabel");
        readonly Func<AppQuery, AppQuery> _logoutLink = e => e.Marked("MenuContainer").Descendant().Marked("LogoutLabel");

        readonly Func<AppQuery, AppQuery> _caredirectorOptionsLabel = e => e.Marked("DefaultOptionsLabel").Text("CAREDIRECTOR");




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
            _app.WaitForElement(_serviceEndpointsLink);
            _app.Tap(_serviceEndpointsLink);

            return new ServiceEndpointsPage(_app);
        }
        
        public AboutPage NavigateToAboutPage()
        {
            _app.Tap(_mainMenuButton);
            _app.WaitForElement(_aboutLink);
            _app.Tap(_aboutLink);

            return new AboutPage(_app);
        }

        public SettingsPage NavigateToSettingsPage()
        {
            
            this.Tap(_mainMenuButton);
            this.WaitForElement(_caredirectorOptionsLabel);

            bool settingsLinkVisible = this.CheckIfElementVisible(_settingsLink);
            if(!settingsLinkVisible)
                this.Tap(_caredirectorOptionsLabel);

            this.Tap(_settingsLink);

            return new SettingsPage(_app);
        }

       
        public MainMenu Logout()
        {
            this.Tap(_mainMenuButton);
            this.WaitForElement(_caredirectorOptionsLabel);

            bool settingsLinkVisible = this.CheckIfElementVisible(_logoutLink);
            if (!settingsLinkVisible)
                this.Tap(_caredirectorOptionsLabel);

            this.Tap(_logoutLink);

            return this;
        }

        public PeoplePage NavigateToPeoplePage()
        {
            WaitForElement(_mainMenuButton);
            TapOnElementWithWidthAndHeight(_mainMenuButton);
            WaitForElement(_peopleLink);
            Tap(_peopleLink);

            return new PeoplePage(_app);
        }

        public MainMenu NavigateToCasesPage()
        {
            _app.Tap(_mainMenuButton);
            _app.WaitForElement(_casesLink);
            _app.Tap(_casesLink);

            return this;
        }

        public PeoplePage NavigateToHomePage()
        {
            this.Tap(_mainMenuButton);
            this.WaitForElement(_homeLink);
            this.Tap(_homeLink);

            return new PeoplePage(_app);
        }

        public MainMenu NavigateToAppointmentsPage()
        {
            _app.Tap(_mainMenuButton);
            _app.WaitForElement(_appointmentsLink);
            _app.Tap(_appointmentsLink);

            return this;
        }
    }
}
