using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;

namespace CareCloudTestFramework.PageObjects
{
    public class BookingDetailsPage : CommonMethods
    {

        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _BookingDetails = e => e.Marked("booking-details-card-body");

        readonly Func<AppQuery, AppQuery> _startVisitButton = e => e.Marked("start-visit-button");

        public BookingDetailsPage(IApp app)
        {
            this._app = app;
        }
        public BookingDetailsPage WaitForBookingDetailsPageToLoad()
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            CheckIfElementVisible(_BookingDetails);


            return this;
        }

        public BookingDetailsPage TapStartVisitButton()
        {
            _app.Tap(x => x.XPath("//*[@id='is-care-plan-reviewed']"));
            _app.Tap(x => x.XPath("//*[@id='start-visit-button']"));

            return this;
        }
    }
}
