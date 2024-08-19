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
    public class MyBookingsPage:CommonMethods
    {

        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _homepageTitle = e => e.Marked("booking-list-title").Text("My Bookings");
        
        readonly Func<AppQuery, AppQuery> _dateofBookingList = e => e.Marked("date-of-booking-list-text");
        readonly Func<AppQuery, AppQuery> _previousdateButton = e => e.Marked("prev-date-button");
        readonly Func<AppQuery, AppQuery> _nextdateButton = e => e.Marked("next-date-button");
        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _homepageLogo = e => e.Marked("EntityImage");


        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshImage");

        readonly Func<AppQuery, AppQuery> _BookingLabel = e => e.Class("mcc-card__body mu-d-flex mu-gap-05 mu-align-items-center mu-w-100 mu-mb-00 mu-pb-05");

        readonly Func<AppQuery, AppQuery> _BookingStatus = e => e.Text("PLANNED");
        readonly Func<AppQuery, AppQuery> _BookingDetails = e => e.Marked("booking-details-card-body");

        readonly Func<AppQuery, AppQuery> _startVisitButton = e => e.Marked("start-visit-button");

        public MyBookingsPage(IApp app)
        {
            this._app = app;
        }
        public MyBookingsPage WaitForMyBookingsPageToLoad()
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            TryScrollToElement(_homepageTitle);
            CheckIfElementVisible(_homepageTitle);
            CheckIfElementVisible(_dateofBookingList);
            CheckIfElementVisible(_previousdateButton);
            CheckIfElementVisible(_nextdateButton);


            return this;
        }

        public MyBookingsPage TapBookingRecord(String recordid)
        {
            _app.ScrollDownTo(x => x.XPath("//*[@data-id='" + recordid + "']"));
            _app.WaitForElement(x => x.XPath("//*[@data-id='" + recordid + "']"));
            CheckIfElementVisible(_BookingStatus);
 
            _app.Tap(x => x.XPath("//*[@data-id='"+recordid+"']"));

            return this;
        }

        public MyBookingsPage TapStartVisitButton()
        {
            CheckIfElementVisible(_BookingDetails);
            _app.Tap(x => x.XPath("//*[@id='start-visit-button']"));
           
            return this;
        }

        public MyBookingsPage VerifyBookingDetails(String recordid)
        {
            _app.ScrollDownTo(x => x.XPath("//*[@data-id='" + recordid + "']"));
            _app.WaitForElement(x => x.XPath("//*[@data-id='" + recordid + "']"));
            CheckIfElementVisible(_BookingStatus);
            CheckIfElementVisible(x=>x.XPath("//button[@data-id='" + recordid + "']/div/span[text()='COMPLETED']"));

            return this;
        }

        public MyBookingsPage VerifyPlannedBookingDetails(String recordid)
        {
            _app.ScrollDownTo(x => x.XPath("//*[@data-id='" + recordid + "']"));
            _app.WaitForElement(x => x.XPath("//*[@data-id='" + recordid + "']"));
            CheckIfElementVisible(_BookingStatus);
            CheckIfElementVisible(x => x.XPath("//button[@data-id='" + recordid + "']/div/span[text()='PLANNED']"));

            return this;
        }
    }
}
