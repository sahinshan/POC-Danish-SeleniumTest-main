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
    public class ActivitiesLookUp : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppWebQuery> _confirActivitiesButton = e => e.XPath("//span[text()='Confirm Activity(s)']");

        readonly Func<AppQuery, AppWebQuery> _cancelButton = e => e.XPath("//button[@class='mcc-button mcc-button--lg mcc-button--block mcc-button--outline cc-person-daily-record-cancel-btn']/span[text()='Cancel']");
        readonly Func<AppQuery, AppWebQuery> _searchLookUpText = e => e.XPath("//*[@id='search-lookup']");
        Func<AppQuery, AppWebQuery> _selctActivity(string dataid) => e => e.XPath("//input[@value='" + dataid + "']/parent::span/parent::label");


        public ActivitiesLookUp(IApp app)
        {
            _app = app;

        }


        public ActivitiesLookUp WaitForActivitiesLookupPopupToLoad()
        {
            WaitForElement(_confirActivitiesButton);
            WaitForElement(_cancelButton);
            WaitForElement(_searchLookUpText);

            return this;
        }

        public ActivitiesLookUp setActivitySearchText(string activities)
        {
            _app.ClearText(_searchLookUpText);
            _app.DismissKeyboard();
            _app.EnterText(_searchLookUpText, activities);
            _app.DismissKeyboard();

            return this;
        }

        public ActivitiesLookUp selectActivity(string dataid)
        {
            WaitForElement(_selctActivity(dataid));
            _app.Tap(_selctActivity(dataid));

            return this;
        }

        public ActivitiesLookUp TapConfirActivities()
        {
            _app.Tap(_confirActivitiesButton);

            return this;
        }
    }
}
