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
    public class VisitCompletedPage : CommonMethods
    {

        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _visitCompleted = e => e.Marked("visit-completed-card-header");

        readonly Func<AppQuery, AppWebQuery> _visitCompletedHeader = e => e.XPath("//div[@class='mu-d-flex mu-flex-column mu-align-items-center']//h2[text()='Visit Completed']");
        readonly Func<AppQuery, AppWebQuery> _visitSummary = e => e.XPath("//div[@id='visit-completed-card-body']");
        readonly Func<AppQuery, AppWebQuery> _taskCompletedHeader = e => e.XPath("//div/h5[text()='Tasks Completed']");
        readonly Func<AppQuery, AppWebQuery> _clientDetailsText = e => e.XPath("//div[@id='visit-completed-card-body']//div[@class='h5 mu-mb-00']");
        readonly Func<AppQuery, AppWebQuery> _backToBookingsButton = e => e.XPath("//div/button[@id='back-to-bookings-btn']");


        Func<AppQuery, AppWebQuery> _taskCompleted(String task) => e => e.XPath("//div[@id='completed-care-tasks-list']//div[text()='"+ task + "']");

        

        Func<AppQuery, AppWebQuery> _DeleteCareTask_Btn(string caretaskid) => e => e.XPath("//*[@data-id='" + caretaskid + "']/following-sibling::button");
        public VisitCompletedPage(IApp app)
        {
            this._app = app;
        }

        public VisitCompletedPage WaitFor_visitCompletedPageToLoad()
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            CheckIfElementVisible(_visitCompleted);


            return this;
        }

        public VisitCompletedPage ValidateVisitDetailsHeader()
        {

            CheckIfElementVisible(_visitCompletedHeader);
            CheckIfElementVisible(_visitSummary);
            CheckIfElementVisible(_taskCompletedHeader);

            return this;
        }

        public VisitCompletedPage ValidateVisitCompletedClientDetails(string ExpectText)
        {


            WaitForElement(_clientDetailsText);
            string fieldText = GetWebElementText(_clientDetailsText);
            Assert.AreEqual(ExpectText, fieldText);


            return this;
        }

        public VisitCompletedPage clickBackToBookings()
        {


            WaitForElement(_backToBookingsButton);
            _app.Tap(_backToBookingsButton);


            return this;
        }



    }
}
