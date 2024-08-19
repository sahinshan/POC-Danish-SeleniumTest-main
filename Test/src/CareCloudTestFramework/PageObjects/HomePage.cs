using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;

namespace CareCloudTestFramework.PageObjects
{
    public class HomePage:CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _homepageLogo = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _homepageTitle = e => e.Text("HOME");

        Func<AppQuery, AppQuery> _viewPicker(string DashboardPicklistName) => e => e.Marked("ViewPicker_Container").Descendant().Marked(DashboardPicklistName);
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshImage");


        Func<AppQuery, AppQuery> _dashboardWidgetTitle(string DashboardWidgetTitle) => e => e.Marked(DashboardWidgetTitle + "_DashboardTitle");
        Func<AppQuery, AppQuery> _dashboardRefereshButton(string RecordTypeName) => e => e.Marked(RecordTypeName + "_RefreshToolbarItem");
        Func<AppQuery, AppQuery> _dashboardNewRecordButton(string RecordTypeName) => e => e.Marked(RecordTypeName + "_NewRecordButton");


        //progress bars
        readonly Func<AppQuery, AppQuery> _LoadingLabel = e => e.Marked("LoadingLabel");
        readonly Func<AppQuery, AppQuery> _ProgressLabel = e => e.Marked("LoadingLabel");



        public HomePage(IApp app)
        {
            _app = app;

        }

        public HomePage WaitForHomePageToLoad()
        {
            _app.WaitForElement(_mainMenuButton, "Unable to find the element: MenuIcon", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_applicationTopIcon, "Unable to find the element: AppIcon", _timeoutTimeSpan, _retryFrequency);
            

            //wait for any sync operation to finish
            WaitForElementNotVisible(_LoadingLabel);
            System.Threading.Thread.Sleep(3000);
            WaitForElementNotVisible(_ProgressLabel);
            System.Threading.Thread.Sleep(3000);

            return this;
        }


        public HomePage WaitForDashboardToLoad(string DashboardPicklistName)
        {
            this.WaitForElement(_viewPicker(DashboardPicklistName));
            this.WaitForElement(_refreshButton);

            return this;
        }

        public HomePage WaitForDashboardWidgetToLoad(string DashboardWidgetTitle, string RecordTypeName, bool NewRecorButtonVisible)
        {
            this.ScrollToElement(_dashboardWidgetTitle(DashboardWidgetTitle));
            this.WaitForElement(_dashboardWidgetTitle(DashboardWidgetTitle));

            this.ScrollToElement(_dashboardRefereshButton(RecordTypeName));
            this.WaitForElement(_dashboardRefereshButton(RecordTypeName));

            if (NewRecorButtonVisible)
            {
                this.ScrollToElement(_dashboardNewRecordButton(RecordTypeName));
                this.WaitForElement(_dashboardNewRecordButton(RecordTypeName));
            }

            if (this.GetElementText(_dashboardWidgetTitle(DashboardWidgetTitle)) != DashboardWidgetTitle)
                throw new Exception("Dashboard title do not match");

            return this;
        }

        public HomePage ValidateDashboardWidgetNotVisible(string DashboardWidgetTitle, string RecordTypeName)
        {
            this.WaitForElementNotVisible(_dashboardWidgetTitle(DashboardWidgetTitle));

            this.WaitForElementNotVisible(_dashboardRefereshButton(RecordTypeName));

            this.WaitForElementNotVisible(_dashboardNewRecordButton(RecordTypeName));

            return this;
        }


    }
}
