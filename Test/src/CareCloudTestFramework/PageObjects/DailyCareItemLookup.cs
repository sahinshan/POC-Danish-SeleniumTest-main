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
    public class DailyCareItemLookup : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");
        Func<AppQuery, AppWebQuery> _header => e => e.XPath("//div[@class='mu-d-flex mu-flex-column mu-gap-06']/h2[text()='Select New Daily Care Item']");
        Func<AppQuery, AppWebQuery> _cancelBtn => e => e.XPath("//div[@class='mu-fw-bolder mu-fs-xl mu-m-00 text']");
        Func<AppQuery, AppWebQuery> _taskIcon => e => e.XPath("//*[@name='task']");
        Func<AppQuery, AppWebQuery> _NonSchedule_DailyCareDrawer => e => e.XPath("//*[@id='non-scheduled-daily-care-drawer']");
        Func<AppQuery, AppWebQuery> _PersonInfoHeader(string personfullname, int number) => e => e.XPath("//span[text()='" + personfullname + " (ID " + number + ")']");
        Func<AppQuery, AppWebQuery> _DailyRecordIcon => e => e.XPath("//div[@class='mu-h-07 mu-d-flex mu-align-items-center mu-justify-content-center mu-border mu-border-02 mu-radius-02']//h3[text()='Daily Record']");
        Func<AppQuery, AppWebQuery> _RepositioningIcon => e => e.XPath("//div[@class='mu-h-07 mu-d-flex mu-align-items-center mu-justify-content-center mu-border mu-border-02 mu-radius-02']//h3[text()='Repositioning']");


        public DailyCareItemLookup(IApp app)
        {
            _app = app;

        }


        public DailyCareItemLookup WaitForDailyCareItemLookupPopupToLoad(string personfullname,int number)
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            WaitForElement(_PersonInfoHeader(personfullname, number));
            WaitForElement(_NonSchedule_DailyCareDrawer);
            WaitForElement(_DailyRecordIcon);
            WaitForElement(_RepositioningIcon);

            return this;
        }

        public DailyCareItemLookup TapDailyRecordIcon()
        {
            
            WaitForElement(_DailyRecordIcon);
            Tap(_DailyRecordIcon);

            return this;
        }


    }
}
